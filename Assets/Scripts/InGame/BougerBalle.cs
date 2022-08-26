using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class BougerBalle : MonoBehaviourPun
{
    public PhotonView PV;


    public void ShootBullet(Vector2 shootingDirection)
    {
        GetComponent<Rigidbody2D>().AddForce(shootingDirection * 10, ForceMode2D.Impulse);
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

            if (!col.gameObject.GetComponent<PhotonView>().IsMine)
            {
                GameObject effect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ExplosionDeBalle"), transform.position, Quaternion.identity);
                Destroy(effect, 1f);
                PhotonNetwork.Destroy(gameObject);
            }

            

            
        }
        
    }
}
