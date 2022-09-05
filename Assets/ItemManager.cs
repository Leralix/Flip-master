using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class ItemManager : MonoBehaviour
{

    public static ItemManager itemManager;

    public List<Item> ItemList = new List<Item>();
    Transform[] ItemSpawnPoints;

    public void Awake()
    {
        ItemSpawnPoints = GetComponentsInChildren<Transform>();

        int position = Random.Range(0, ItemSpawnPoints.Length);
        ItemOnGround Holder = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ItemHolder"), ItemSpawnPoints[position].position, Quaternion.identity).GetComponent<ItemOnGround>();
        Holder.GiveItem(ItemList[Random.Range(0, ItemList.Count)]);

    }



}
