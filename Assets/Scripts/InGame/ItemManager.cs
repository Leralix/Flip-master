using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ItemManager : MonoBehaviour
{

    public static ItemManager itemManager;

    public List<ItemInfo> ItemList = new List<ItemInfo>();
    Transform[] ItemSpawnPoints;

    public void Awake()
    {
        ItemSpawnPoints = GetComponentsInChildren<Transform>();

        InvokeRepeating("spawnWeapon", 0, 10);

    }

    public void spawnWeapon()
    {
        int position = Random.Range(0, ItemSpawnPoints.Length);

        ItemOnGround Holder = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ItemHolder"), ItemSpawnPoints[position].position, Quaternion.identity).GetComponent<ItemOnGround>();
        Holder.GiveItem(ItemList[Random.Range(0, ItemList.Count)]);
        //Holder.GiveItem(ItemList[Random.Range(0, ItemList.Count)]);
    }
}
