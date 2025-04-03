using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager _instance;



    public List<MissionInfoTotal> spawnedMissions = new List<MissionInfoTotal>();
    public List<MissionInfoTotal> activeMissions = new List<MissionInfoTotal>();
    List<Missions> missionsPool = new List<Missions>();

    public int points;
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

        // Grab all missions from the Resources folder and correctly cast it
        var missionsLoaded = Resources.LoadAll("Missions", typeof(Missions)).Cast<Missions>();
        foreach (var mission in missionsLoaded)
        {
            missionsPool.Add(mission);
        }
    }



    // Update is called once per frame
    void Start()
    {
        //Binding events and starting game loop
        StartCoroutine("LoopSpawn");
        InteractControls.Delivered += MissionOver;
        InteractControls.Grabbed += ActivateMission;
    }

    IEnumerator LoopSpawn()
    {
        int i = 0;
        while (i < 5)
        {
            newMissionAttempt();
            yield return new WaitForSeconds(2f);
            i++;
        }
    }

    //Activates when pickedup
    public void ActivateMission(MissionInfoTotal mission)
    {
        activeMissions.Add(mission);
        spawnedMissions.Remove(mission);
    }

     /* When dropped off, it checks if the item and street are correct. We need to delete *A* Mission so we grab 
     * whatever the most recent item is (which is also the one we drop first)
     * Then, if the item is on the right street we give it the correct amount of points, otherwise its halved 
     * I intended to have time since pickup effect its points, but i wanted to get this to yall early
     */
    public void MissionOver(PickUps dropoff, Streets correctStreet)
    {

        MissionInfoTotal correctMission = activeMissions[activeMissions.Count-1];
        correctMission.info.initialWorth = -1;

        int correctness = 1;
        foreach (MissionInfoTotal mission in activeMissions)
        {
            if (correctMission.info.prefab.GetComponent<PickUps>().publicName == dropoff.publicName)
            {
                if (mission.dropoffEnum == correctStreet)
                {
                    correctness = 2;
                    correctMission = mission;
                    break;
                }
            }
            Debug.Log(mission.info.prefab.GetComponent<PickUps>().publicName + " | " + mission.dropoffEnum + "\n" + dropoff.publicName + " | " + correctStreet);

            //Debug.Log(correctness);
        }

        if (correctness == 2)
        {
            points += correctMission.info.initialWorth;
        }
        else if (correctness == 1)
        {
            points += correctMission.info.initialWorth / 2;
        } else
        {
            points += 0;
        }

        activeMissions.Remove(correctMission);
    }

     /* When a new mission is made, we randomly select from the preset items we have and assign it a spawn and dropoff location.
     *  Since we cant spawn nowhere, we make sure the spawn isn't null, otherwise all spawns are filled up.
     *  After that, we instantiate the object we selected from our mission pool and pass THE SPAWNED OBJECT the mission data as well for future purposes
     */

    bool newMissionAttempt()
    {
        Missions missionChosen = missionsPool[Random.Range(0, missionsPool.Count)];
        MissionInfoTotal total = new MissionInfoTotal();
        total.info = missionChosen.info;
        //Debug.Log(total.info.missionText);
        GameObject spawn = SpawnManager._instance.AvailableSpawns(-1);
        //Debug.Log(spawn.name);
        Streets dropEnum;
        if (spawn != null)
        {
            total.spawnLocation = spawn;
            total.dropoffLocation = SpawnManager._instance.AvailableDropoffs(-1, out dropEnum);
            spawnedMissions.Add(total);
            //Debug.Log(total.dropoffLocation.gameObject.name);
            //Debug.Log(total.spawnLocation.name);
            //Debug.Log(missionChosen.info.prefab.name);
            GameObject spawnObject = Instantiate(missionChosen.info.prefab, 
                total.spawnLocation.transform.position,
                total.spawnLocation.transform.rotation);
            total.dropoffLocation.GetComponent<Dropoffs>().Activate();
            total.dropoffEnum = dropEnum;

            PickUps pickUpRef = spawnObject.GetComponent<PickUps>();
            pickUpRef.missionData = total;
            //activeMissions.Add(total);
            return true;
        }
        return false;
    }
}
