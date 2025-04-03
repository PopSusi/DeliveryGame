using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Android.Gradle.Manifest;

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

    public List<GameObject> unavailableYellowSpawnPoints = new List<GameObject>();
    public List<GameObject> unavailableRedSpawnPoints = new List<GameObject>();
    public List<GameObject> unavailableGreenSpawnPoints = new List<GameObject>();
    public List<GameObject> unavailableBlueSpawnPoints = new List<GameObject>();
    public List<GameObject> unavailableBrownSpawnPoints = new List<GameObject>();

    public List<GameObject> unavailableYellowDropOffs = new List<GameObject>();
    public List<GameObject> unavailableRedDropOffs = new List<GameObject>();
    public List<GameObject> unavailableGreenDropOffs = new List<GameObject>();
    public List<GameObject> unavailableBlueDropOffs = new List<GameObject>();
    public List<GameObject> unavailableBrownDropOffs = new List<GameObject>();

    public Dictionary<Streets, List<GameObject>> associatedSpawns = new Dictionary<Streets, List<GameObject>>();
    public Dictionary<Streets, List<GameObject>> associatedDropoffs = new Dictionary<Streets, List<GameObject>>();

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
        associatedSpawns.Add(Streets.Yellow, availableYellowSpawnPoints);
        associatedDropoffs.Add(Streets.Yellow, availableYellowDropOffs);
        associatedSpawns.Add(Streets.Red, availableRedSpawnPoints);
        associatedDropoffs.Add(Streets.Red, availableRedDropOffs);
        associatedSpawns.Add(Streets.Green, availableGreenSpawnPoints);
        associatedDropoffs.Add(Streets.Green, availableGreenDropOffs);
        associatedSpawns.Add(Streets.Blue, availableBlueSpawnPoints);
        associatedDropoffs.Add(Streets.Blue, availableBlueDropOffs);
        associatedSpawns.Add(Streets.Brown, availableBrownSpawnPoints);
        associatedDropoffs.Add(Streets.Brown, availableBrownDropOffs);
    }

    private void Start()
    {
        
    }


    /* -- The following two functions are very similar, and basically comb through each spawn point list
     *    and then return the spawn/dropoff
     */
    public GameObject AvailableSpawns(int streetPriority)
    {
        GameObject spawnpoint = null;
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            //Debug.Log(streetPriority);
        }
        Streets streetEnum = (Streets)streetPriority;
        associatedSpawns.TryGetValue(streetEnum, out var availableSpawns);
        if (availableSpawns.Count != 0)
        {
            spawnpoint = availableSpawns[UnityEngine.Random.Range(0, availableSpawns.Count)];
        }
        else
        {
            spawnpoint = AvailableSpawns((streetPriority + 1) % 5);
        }

        try
        {
            Debug.Log(spawnpoint.name);
        }
        catch (Exception ex)
        {
            foreach (Streets item in associatedSpawns.Keys)
            {
                associatedSpawns.TryGetValue(item, out List<GameObject> newlist);
                Debug.Log(item.ToString() + "|" + newlist.Count);
            }
        }
        return spawnpoint;

    }

    public GameObject AvailableDropoffs(int streetPriority, out Streets streetOut)
    {
        GameObject spawnpoint = null;
        streetOut = Streets.Debug;
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            //Debug.Log(streetPriority);
        }
        Streets streetEnum = (Streets)streetPriority;
        associatedDropoffs.TryGetValue(streetEnum, out var availableDropoffPoints);
        if (availableDropoffPoints.Count != 0)
        {
            spawnpoint = availableDropoffPoints[UnityEngine.Random.Range(0, availableDropoffPoints.Count)];
        }
        else
        {
            spawnpoint = AvailableDropoffs((streetPriority + 1) % 5, out streetOut);
        }

        try
        {
            Debug.Log(spawnpoint.name);
        }
        catch (Exception ex)
        {
            foreach (Streets item in associatedSpawns.Keys)
            {
                associatedSpawns.TryGetValue(item, out List<GameObject> newlist);
                Debug.Log(item.ToString() + "|" + newlist.Count);
            }
        }
        return spawnpoint;

    }


    // ---- Old boilerplate spawning code ---- //
    // Update is called once per frame
    /*public GameObject FindAvailableSpawn(int streetPriority)
    {
        GameObject spawnPoint = null;
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            //Debug.Log(streetPriority);
        }
        switch (streetPriority)
        {
            case 0:
                if (availableYellowSpawnPoints.Count != 0)
                {
                    spawnPoint = availableYellowSpawnPoints[UnityEngine.Random.Range(0, math.clamp(availableYellowSpawnPoints.Count, 0, 5))];
                    availableYellowSpawnPoints.Remove(spawnPoint);
                    unavailableYellowSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(1);
                }
                break;
            case 1:
                if (availableRedSpawnPoints.Count != 0)
                {
                    spawnPoint = availableRedSpawnPoints[UnityEngine.Random.Range(0, math.clamp(availableRedSpawnPoints.Count, 0, 5))];
                    availableRedSpawnPoints.Remove(spawnPoint);
                    unavailableRedSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(2);
                }
                break;
            case 2:
                if (availableGreenSpawnPoints.Count != 0)
                {
                    spawnPoint = availableGreenSpawnPoints[UnityEngine.Random.Range(0, math.clamp(availableGreenSpawnPoints.Count, 0, 5))];
                    availableGreenSpawnPoints.Remove(spawnPoint);
                    unavailableGreenSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(3);
                }
                break;
            case 3:
                if (availableBlueSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBlueSpawnPoints[UnityEngine.Random.Range(0, math.clamp(availableBlueSpawnPoints.Count, 0, 5))];
                    availableBlueSpawnPoints.Remove(spawnPoint);
                    unavailableBlueSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableSpawn(4);
                }
                break;
            case 4:
                if (availableBrownSpawnPoints.Count != 0)
                {
                    spawnPoint = availableBrownSpawnPoints[UnityEngine.Random.Range(0, math.clamp(availableBrownSpawnPoints.Count, 0, 5))];
                    availableBrownSpawnPoints.Remove(spawnPoint);
                    unavailableBrownSpawnPoints.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = null;
                }
                break;
        }
        return spawnPoint;
    }
    public GameObject FindAvailableDropoff(int streetPriority, out Streets streetOut)
    {
        streetOut = Streets.Debug;
        if (streetPriority == -1)
        {
            streetPriority = UnityEngine.Random.Range(0, 5);
            //Debug.Log(streetPriority);
        }
        GameObject spawnPoint = null;
        switch (streetPriority)
        {
            case 0:
                if (availableYellowDropOffs.Count != 0)
                {
                    spawnPoint = availableYellowDropOffs[UnityEngine.Random.Range(0, availableYellowDropOffs.Count)];
                    availableYellowDropOffs.Remove(spawnPoint);
                    unavailableYellowDropOffs.Add(spawnPoint);
                    streetOut = Streets.Yellow;
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(1, out streetOut);
                }
                break;
            case 1:
                if (availableRedDropOffs.Count != 0)
                {
                    spawnPoint = availableRedDropOffs[UnityEngine.Random.Range(0, availableRedDropOffs.Count)];
                    availableRedDropOffs.Remove(spawnPoint);
                    unavailableRedDropOffs.Add(spawnPoint);
                    streetOut = Streets.Red;
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(2, out streetOut);
                }
                break;
            case 2:
                if (availableGreenDropOffs.Count != 0)
                {
                    streetOut = Streets.Green;
                    spawnPoint = availableGreenDropOffs[UnityEngine.Random.Range(0, availableGreenDropOffs.Count)];
                    availableGreenDropOffs.Remove(spawnPoint);
                    unavailableGreenDropOffs.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(3, out streetOut);
                }
                break;
            case 3:
                if (availableBlueDropOffs.Count != 0)
                {
                    streetOut = Streets.Blue;
                    spawnPoint = availableBlueDropOffs[UnityEngine.Random.Range(0, availableBlueDropOffs.Count)];
                    availableBlueDropOffs.Remove(spawnPoint);
                    unavailableBlueDropOffs.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(4, out streetOut);
                }
                break;
            case 4:
                if (availableBrownDropOffs.Count != 0)
                {
                    streetOut = Streets.Brown;
                    spawnPoint = availableBrownDropOffs[UnityEngine.Random.Range(0, availableBrownDropOffs.Count)];
                    availableBrownDropOffs.Remove(spawnPoint);
                    unavailableBrownDropOffs.Add(spawnPoint);
                }
                else
                {
                    spawnPoint = FindAvailableDropoff(0, out streetOut);
                }
                break;
            default:
                streetOut = Streets.Brown;
                break;
        }
        //Debug.Log(spawnPoint.name);
        return spawnPoint;
    }
    */
}
