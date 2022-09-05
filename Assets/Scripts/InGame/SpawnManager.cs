using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    SpawnPoint[] spawnpoints;

    private void Awake()
    {
        instance = this;
        spawnpoints = GetComponentsInChildren<SpawnPoint>();
    }

    public Transform getSpawnPoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
    }

}
