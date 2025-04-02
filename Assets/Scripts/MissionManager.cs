using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager _instance;



    public List<MissionInfoTotal> spawnedMissions = new List<MissionInfoTotal>();
    public List<MissionInfoTotal> activeMissions = new List<MissionInfoTotal>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
    void Start()
    {
        StartCoroutine("LoopSpawn");
    }

    IEnumerator LoopSpawn()
    {
        while (true)
        {
            newMissionAttempt();
            yield return new WaitForSeconds(2f);
        }
    }

    bool newMissionAttempt()
    {
        var missionsLoaded = Resources.LoadAll("Missions", typeof(Missions)).Cast<Missions>();
        List<Missions> missions = new List<Missions>();
        foreach (var mission in missionsLoaded)
        {
            missions.Add(mission);
        }
        MissionInfoTotal total = new MissionInfoTotal();
        Missions missionChosen = missions[Random.Range(0, missions.Count)];
        total.info = missionChosen.info;
        //Debug.Log(total.info.missionText);
        GameObject spawn = SpawnManager._instance.FindAvailableSpawn(-1);
        //Debug.Log(spawn.name);
        Streets dropEnum;
        if (spawn != null)
        {
            total.spawnLocation = SpawnManager._instance.FindAvailableSpawn(-1);
            total.dropoffLocation = SpawnManager._instance.FindAvailableDropoff(-1, out dropEnum);
            spawnedMissions.Add(total);
            GameObject spawnObject = Instantiate(missionChosen.info.prefab, 
                total.spawnLocation.transform.position,
                total.spawnLocation.transform.rotation);
            total.dropoffLocation.GetComponent<Dropoffs>().Activate();
            total.dropoffEnum = dropEnum;
            spawnObject.GetComponent<PickUps>().missionData = total;
            return true;
        }
        return false;
    }
}
