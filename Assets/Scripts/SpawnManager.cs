using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> availableYellowSpawnPoints = new List<GameObject>();
    public List<GameObject> availableRedSpawnPoints = new List<GameObject>();
    public List<GameObject> availableGreenSpawnPoints = new List<GameObject>();
    public List<GameObject> availableBlueSpawnPoints = new List<GameObject>();
    public List<GameObject> availableBrownSpawnPoints = new List<GameObject>();

    public List<GameObject> availableYellowDropOffs = new List<GameObject>();
    public List<GameObject> availableRedDropOffs = new List<GameObject>();
    public List<GameObject> availableGreenDropOffs = new List<GameObject>();
    public List<GameObject> availableBlueDropOffs = new List<GameObject>();
    public List<GameObject> availableBrownDropOffs = new List<GameObject>();

    public static SpawnManager _instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    public GameObject FindAvailableSpawn(int streetPriority)
    {
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            Debug.Log(streetPriority);
        }
        GameObject spawnPoint = null;
        switch (streetPriority)
        {
            case 0:
                if (availableYellowSpawnPoints.Count != 0)
                {
                    spawnPoint = availableYellowSpawnPoints[UnityEngine.Random.Range(0, availableYellowSpawnPoints.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(1);
                }
                break;
            case 1:
                if (availableRedSpawnPoints.Count != 0)
                {
                    spawnPoint = availableRedSpawnPoints[UnityEngine.Random.Range(0, availableRedSpawnPoints.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(2);
                }
                break;
            case 2:
                if (availableGreenSpawnPoints.Count != 0)
                {
                    spawnPoint = availableGreenSpawnPoints[UnityEngine.Random.Range(0, availableGreenSpawnPoints.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(3);
                }
                break;
            case 3:
                if (availableBlueSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBlueSpawnPoints[UnityEngine.Random.Range(0, availableBlueSpawnPoints.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(4);
                }
                break;
            case 4:
                if (availableBrownSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBrownSpawnPoints[UnityEngine.Random.Range(0, availableBrownSpawnPoints.Count)];
                }
                else
                {
                    spawnPoint = null;
                }
                break;
        }
        return spawnPoint;
    }
    public GameObject FindAvailableDropoff(int streetPriority)
    {
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            //Debug.Log(streetPriority);
        }
        GameObject spawnPoint = null;
        switch (streetPriority)
        {
            case 0:
                if (availableYellowSpawnPoints.Count != 0)
                {
                    spawnPoint = availableYellowDropOffs[UnityEngine.Random.Range(0, availableYellowDropOffs.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(1);
                }
                break;
            case 1:
                if (availableRedSpawnPoints.Count != 0)
                {
                    spawnPoint = availableRedDropOffs[UnityEngine.Random.Range(0, availableRedDropOffs.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(2);
                }
                break;
            case 2:
                if (availableGreenSpawnPoints.Count != 0)
                {
                    spawnPoint = availableGreenDropOffs[UnityEngine.Random.Range(0, availableGreenDropOffs.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(3);
                }
                break;
            case 3:
                if (availableBlueSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBlueDropOffs[UnityEngine.Random.Range(0, availableBlueDropOffs.Count)];
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(4);
                }
                break;
            case 4:
                if (availableBrownSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBrownDropOffs[UnityEngine.Random.Range(0, availableBrownDropOffs.Count)];
                }
                else
                {
                    spawnPoint = null;
                }
                break;
        }
        Debug.Log(spawnPoint.name);
        return spawnPoint;
    }
}
