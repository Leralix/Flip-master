using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using HashTable = ExitGames.Client.Photon.Hashtable; //ne fonctione qu'à moitié pas, j'ai copié cette ligne a la création de la Hashtable du coup.
using Photon.Realtime;

public class Shooting : MonoBehaviourPunCallbacks
{

    [SerializeField] Item[] items;

    int itemIndex;
    int previousItemIndex = -1;


    public Transform GunBarrel;
    public Transform TriggerPoint;

    public PhotonView PV;

    private int rotationOffset = 0;
    private Quaternion gunPos;
    private Vector3 difference;


    private void Awake()
    {
        if (PV.IsMine)
        {
            
        }
    }

    private void Start()
    {
        if(PV.IsMine)
        {
            EquipItem(0);
        }
    }
    void Update()
    {
        if (PV.IsMine)
        {

            RotateArm();
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                items[itemIndex].Use();
            }
                
            for (int i = 0; i < items.Length; i++)
            {
                if(Input.GetKeyDown((i + 1).ToString()))
                {
                    EquipItem(i);
                    break;
                }
            }

            if(Input.GetAxisRaw("Mouse ScrollWheel")> 0f)
            {
                if(previousItemIndex < items.Length - 1)
                    EquipItem(previousItemIndex + 1);
            }
            else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
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

    void EquipItem(int _index) 
    {
        if (_index == previousItemIndex)
            return;
        
        itemIndex = _index;

        items[itemIndex].ItemGameObject.SetActive(true);
        if (previousItemIndex != -1)
        {
            items[previousItemIndex].ItemGameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if(PV.IsMine)
        {
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            if(items[itemIndex].GetType().IsSubclassOf(typeof(Gun)))
            {
                Gun currentgun = (Gun)items[itemIndex];
                GunBarrel = currentgun.GetShootingPoint();

            }

        }

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps)
    {
        if(!PV.IsMine && targetPlayer == PV.Owner)
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


    private void Shoot()
    {     

        GameObject Bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Balle"), GunBarrel.position, TriggerPoint.rotation);//Quaternion.Euler(GunBarrel.rotation.x, GunBarrel.rotation.y, GunBarrel.rotation.z - 90));

        Bullet.GetComponent<BougerBalle>().InitialiseBullet(GunBarrel.right, ((GunInfo)items[itemIndex].itemInfo).BulletDamage, -1);
    }

    
}
