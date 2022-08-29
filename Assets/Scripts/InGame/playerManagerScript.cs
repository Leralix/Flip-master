using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class playerManagerScript : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(0,2,0), Quaternion.identity, 0, new object[] { PV.ViewID } );
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();  
    }
}
