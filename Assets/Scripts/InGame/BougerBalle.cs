using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class BougerBalle : MonoBehaviourPun
{
    public PhotonView PV;
    int bulletDamage;
    int bulletSpeed;


    public void InitialiseBullet(Vector2 shootingDirection, int _bulletDamage,int _bulletspeed, float spread)
    {
        GetComponent<Rigidbody2D>().AddForce(this.transform.right * 10, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().rotation = spread;
        bulletDamage = _bulletDamage;
        bulletSpeed = _bulletspeed;
    }

    [PunRPC]
    public void DestroyObject()
    {
        GameObject effect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ExplosionDeBalle"), transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        PhotonNetwork.Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D col)
    {

        if(!PV.IsMine)
        {
            return;
        }
        PhotonView target = col.gameObject.GetComponent<PhotonView>();


        if (col.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            GameObject effect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ExplosionDeBalle"), transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            PhotonNetwork.Destroy(gameObject);


        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (col.gameObject.GetComponent<PhotonView>().IsMine) 
                return;
            
            GameObject effect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ExplosionDeBalle"), transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            PhotonNetwork.Destroy(gameObject);

            

            col.gameObject.GetComponent<IDamageable>()?.TakeDamage(bulletDamage);



        }
        
    }
}
