using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Mouvement : MonoBehaviour, IDamageable
{
    public GameObject respawnPoint;

    public int movementSpeed;
    public int movementRatioBase;
    private int movementRatio;
    private float mouvementMultiplier;
    public int jumpPower;
    private bool isGrounded, isOnWallRight, isOnWallLeft;
    public Rigidbody2D Body;

    public Transform groundCheck1, groundCheck2, wallCheckUpRight, wallCheckDownRight, wallCheckUpLeft, wallCheckDownLeft;
    public LayerMask Platform;

    const int maxHealth = 100;
    int currentHealth = maxHealth;

    playerManagerScript playerManager;

    public PhotonView PV;

    private void Awake()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        else
        {
            print("test");
            Camera.main.GetComponent<CameraFollow>().SetActive(this.gameObject);  //Si il y a un bug a cette ligne, c'est parce qu'il n'y a pas le script sur la caméra
            playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<playerManagerScript>(); // pas sûr de le mettre dans le else
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            MovePlayer();
        }
        if(transform.position.y < -100)
        {
            Die();
        }
        

    }

    void MovePlayer()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, Platform);
        
        isOnWallRight = Physics2D.OverlapCircle(wallCheckUpRight.position, 0.2f, Platform) && Physics2D.OverlapCircle(wallCheckDownRight.position, 0.2f, Platform);
        isOnWallLeft = Physics2D.OverlapCircle(wallCheckUpLeft.position, 0.2f, Platform) && Physics2D.OverlapCircle(wallCheckDownLeft.position, 0.2f, Platform);


        if(isGrounded)
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
        if (!Input.GetKey("q") && !Input.GetKey("q"))
        {
            if (mouvementMultiplier > 0)
            {
                mouvementMultiplier -= Time.deltaTime;
            }
            if (mouvementMultiplier < 0)
            {
                mouvementMultiplier += Time.deltaTime;
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if(isGrounded)
            {
                Body.AddForce(Vector2.up * jumpPower);
            }
            else if(isOnWallRight)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = -1f;
            }
            else if(isOnWallLeft)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = 1f;
            }
        }

        //Body.AddForce(Vector2.right * movementSpeed * (mouvementMultiplier + 0.5f) * MovementDirecton);

        mouvementMultiplier = Mathf.Clamp(mouvementMultiplier, -1f, 1f);
        /*if (mouvementMultiplier < -1f)
        {
            mouvementMultiplier = -1;
        }
        else if (mouvementMultiplier > 1f)
        {
            mouvementMultiplier = 1;
        }*/


        Body.velocity = new Vector2(movementSpeed * (mouvementMultiplier), Body.velocity.y);
        
    }


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
}

