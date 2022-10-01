using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using System.IO;
using HashTable = ExitGames.Client.Photon.Hashtable; //ne fonctione qu'à moitié pas, j'ai copié cette ligne a la création de la Hashtable du coup.
using Photon.Realtime;

using System.Linq;

public class Mouvement : MonoBehaviourPunCallbacks, IDamageable, IPlayerController
{
    private Rigidbody2D Body;
    private playerManagerScript playerManager;
    public PhotonView PV;



    public Vector3 Velocity { get; private set; }
    public bool JumpingThisFrame { get; private set; }
    public bool LandingThisFrame { get; private set; }
    public Vector3 RawMovement { get; private set; }
    public bool Grounded => _colDown;

    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed;



    private void Awake()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        else
        {
            gameObject.tag = "MyPlayer";

            Body = GetComponent<Rigidbody2D>();
            //PV = GetComponent<PhotonView>();

            Camera.main.GetComponent<CameraFollow>().SetActive(this.gameObject);  //Si il y a un bug a cette ligne, c'est parce qu'il n'y a pas le script sur la caméra
            playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<playerManagerScript>(); // pas sûr de le mettre dans le else
            currentHealth = maxHealth;
        }    
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            EquipItem(0);
        }
    }

    void Update()
    {
        if(PV.IsMine)
        {
            RunCollisionChecks();
            MovePlayer();
            MoveGun();
            pickItem();
            //AddGravity(); dans fixedUpdate
        }
        if(transform.position.y < -100)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        AddGravity();
    }

    #region Collisions

    [Header("COLLISION")] [SerializeField] private Bounds _characterBounds;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private int _detectorCount = 3;
    [SerializeField] private float _detectionRayLength = 0.1f;
    [SerializeField] [Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f; // Prevents side detectors hitting the ground

    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private bool _colUp, _colRight, _colDown, _colLeft;
    private bool _isOnLeftWall, _isOnRightWall, _OnGround, _onAir;

    private float _timeLeftGrounded;

    // We use these raycast checks for pre-collision information
    private void RunCollisionChecks()
    {
        // Generate ray ranges. 
        CalculateRayRanged();

        // Ground
        LandingThisFrame = false;
        var groundedCheck = RunDetection(_raysDown);
        if (_colDown && !groundedCheck) _timeLeftGrounded = Time.time; // Only trigger when first leaving
        else if (!_colDown && groundedCheck)
        {
            LandingThisFrame = true;
        }

        _colDown = groundedCheck;

        // The rest
        _colUp = RunDetection(_raysUp);
        _colLeft = RunDetection(_raysLeft);
        _colRight = RunDetection(_raysRight);

        bool RunDetection(RayRange range)
        {
            return EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, _detectionRayLength, _groundLayer));
        }
    }

    private void CalculateRayRanged()
    {
        // This is crying out for some kind of refactor. 
        var b = new Bounds(transform.position + _characterBounds.center, _characterBounds.size);

        _raysDown = new RayRange(b.min.x + _rayBuffer, b.min.y, b.max.x - _rayBuffer, b.min.y, Vector2.down);
        _raysUp = new RayRange(b.min.x + _rayBuffer, b.max.y, b.max.x - _rayBuffer, b.max.y, Vector2.up);
        _raysLeft = new RayRange(b.min.x, b.min.y + _rayBuffer, b.min.x, b.max.y - _rayBuffer, Vector2.left);
        _raysRight = new RayRange(b.max.x, b.min.y + _rayBuffer, b.max.x, b.max.y - _rayBuffer, Vector2.right);
    }


    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
    {
        for (var i = 0; i < _detectorCount; i++)
        {
            var t = (float)i / (_detectorCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }

    private void OnDrawGizmos()
    {
        // Bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _characterBounds.center, _characterBounds.size);

        // Rays
        if (!Application.isPlaying)
        {
            CalculateRayRanged();
            Gizmos.color = Color.blue;
            foreach (var range in new List<RayRange> { _raysUp, _raysRight, _raysDown, _raysLeft })
            {
                foreach (var point in EvaluateRayPositions(range))
                {
                    Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                }
            }
        }

        if (!Application.isPlaying) return;

        // Draw the future position. Handy for visualizing gravity
        Gizmos.color = Color.red;
        var move = new Vector3(_currentHorizontalSpeed, _currentVerticalSpeed) * Time.deltaTime;
        Gizmos.DrawWireCube(transform.position + _characterBounds.center + move, _characterBounds.size);
    }

    #endregion

    #region Déplacement

    [Header("DEPLACEMENT")]
    [SerializeField] private int movementSpeed;
    [SerializeField] private int movementRatioBase;
    private int movementRatio;
    private float mouvementMultiplier = 0;
    [SerializeField] private int jumpPower;

    void MovePlayer()
    {
        
        if(_colDown)
        {
            movementRatio = movementRatioBase;
        }
        else
        {
            movementRatio = movementRatioBase / 2;
        }
        
        if (Input.GetKey("d"))
        {

            if(mouvementMultiplier < 0)
            {
                mouvementMultiplier += Time.deltaTime * movementRatio;
            }
            else
            {
                mouvementMultiplier += Time.deltaTime * (movementRatio * 2);
            }

        }
        if (Input.GetKey("q"))
        {
            if (mouvementMultiplier > 0)
            {
                mouvementMultiplier -= Time.deltaTime * movementRatio;
            }
            else
            {
                mouvementMultiplier -= Time.deltaTime * (movementRatio * 2);
            }
            
        }
        if (!Input.GetKey("q") && !Input.GetKey("d"))
        {
            if (mouvementMultiplier > 0.1)
            {
                mouvementMultiplier -= Time.deltaTime * movementRatio;
            }
            else if (mouvementMultiplier < -0.1)
            {
                mouvementMultiplier += Time.deltaTime * movementRatio;
            }
            else
            {
                mouvementMultiplier = 0;
            }
                
        }
        if (Input.GetKeyDown("space"))
        {
            if(_colDown)
            {
                Body.AddForce(Vector2.up * jumpPower);
            }
            else if(_colRight)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = -1f;
            }
            else if(_colLeft)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = 1f;
            }
        }

        mouvementMultiplier = Mathf.Clamp(mouvementMultiplier, -1f, 1f);


        Body.velocity = new Vector2(movementSpeed * (mouvementMultiplier), Body.velocity.y);

    }
    #endregion

    #region gravité
    void AddGravity()
    {
        //float actualTime = Time.time - _timeLeftGrounded;

        _isOnLeftWall = _colRight;
        _isOnRightWall = _colLeft;
        _OnGround = _colDown;
        _onAir = !_colRight && !_colLeft && !_colDown;

        
        if (_isOnLeftWall && Input.GetKey("q")|| _isOnRightWall && Input.GetKey("d")) //Accroché a un mur
        {
            Body.velocity = new Vector2(Body.velocity.x, 0); 
        }
        else if (_isOnLeftWall || _isOnRightWall)
        {
            Body.velocity = new Vector2(Body.velocity.x, 0.1f);
        }
        else if (Input.GetKey("z")) 
        {
            Body.gravityScale = 0.8F;
        }
        else if (Input.GetKey("s"))
        {
            Body.gravityScale = 1.3F;
        }
        
    }
    #endregion

    #region Inventaire et mouvement d'arme

    [Header("TIR & MOUVEMENT")]
    [SerializeField] int maxHealth;
    int currentHealth;

    //List<Item> itemList = new List<Item>();
    public Item[] EquipedItems;

    private bool canPickItem;
    private ItemInfo itemThatCanBePicked;
    private ItemOnGround itemHolderOfTheGun;

    public GunController gunController;
    int itemIndex;
    int previousItemIndex = -1;

    private Transform GunBarrel;
    public Transform TriggerPoint;

    private int rotationOffset = 0;
    private Quaternion gunPos;
    private Vector3 difference;

    void MoveGun()
    {
        if (PV.IsMine)
        {

            RotateArm();

            for (int i = 0; i < EquipedItems.Length; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    EquipItem(i);
                    break;
                }
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (previousItemIndex < EquipedItems.Length - 1)
                    EquipItem(previousItemIndex + 1);
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (previousItemIndex > 0)
                    EquipItem(previousItemIndex - 1);
            }

        }
        else
        {
            SmoothNetMovement();
        }
    }

    public void CanPickItem(ItemInfo itemInfo, ItemOnGround itemHolder)
    {
        canPickItem = true;
        itemThatCanBePicked = itemInfo;
        itemHolderOfTheGun = itemHolder;
    }

    public void CannotPickItem()
    {
        canPickItem = false;
        itemThatCanBePicked = null;
    }

    public void pickItem()
    {
        if(canPickItem && Input.GetKeyDown("e"))
        {
            print(itemThatCanBePicked.GetType());
            if(true) // faut vérif si c'est un Gun ou un Item
            {
                //((Gun)EquipedItems[itemIndex]).ReceiveGunInfo((Gun)itemThatCanBePicked);
                //gunController.ChangeItem((Gun)EquipedItems[itemIndex]);
                //itemHolderOfTheGun.ItemHasBeenPicked();

                itemHolderOfTheGun.ItemHasBeenPicked();
                ((Gun)EquipedItems[itemIndex]).ReceiveGunInfo((GunInfo)itemThatCanBePicked);
                gunController.ChangeItem((Gun)EquipedItems[itemIndex]);

                //GameObject newGun = Instantiate(EquipedItems[itemIndex].gameObject, TriggerPoint.position, TriggerPoint.rotation);
                //newGun.transform.parent = TriggerPoint.transform;
            }
            

            CannotPickItem();
        }
        
    }

    void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;

        EquipedItems[itemIndex].gameObject.SetActive(true);
        if (previousItemIndex != -1)
        {
            EquipedItems[previousItemIndex].gameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if (PV.IsMine)
        {
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            GunBarrel = EquipedItems[itemIndex].aimingPoint;
            

            if (EquipedItems[itemIndex].GetType() == typeof(Gun))
            {
                gunController.ChangeItem((Gun)EquipedItems[itemIndex]);
            }
            
        }

    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps) // utilisé pour synchroniser le changement d'arme
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);


        }
    }
    private void RotateArm()   //Bouger l'arme, coté client
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TriggerPoint.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        TriggerPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
    private void SmoothNetMovement() //Bouger l'arme, coté serveur
    {
        TriggerPoint.rotation = Quaternion.Lerp(TriggerPoint.rotation, gunPos, Time.deltaTime * 8);
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(TriggerPoint.rotation);
        }
        else
        {
            gunPos = (Quaternion)stream.ReceiveNext();
        }
    }
    #endregion

    #region Recevoir des dégats et mourir
    public void TakeDamage(int damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        if (!PV.IsMine)
            return;

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        playerManager.Die();

        Camera.main.GetComponent<CameraFollow>().ResetOnPlayer(this.transform); // Ne fonctionne pas, a modifer avec les point de respawn

    }
    #endregion
}

