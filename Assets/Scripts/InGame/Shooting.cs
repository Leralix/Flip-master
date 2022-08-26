using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Shooting : MonoBehaviour
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

    }

    private void RotateArm()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TriggerPoint.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        TriggerPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
    private void SmoothNetMovement()
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

        Bullet.GetComponent<BougerBalle>().ShootBullet(GunBarrel.right);

        //Bullet.GetComponent<Rigidbody2D>().AddForce(GunBarrel.right * bulletForce, ForceMode2D.Impulse);
    }
}
