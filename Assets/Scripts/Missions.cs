using System.Collections.Generic;
using UnityEngine;

public enum Streets
{
    Yellow,
    Red,
    Green,
    Blue,
    Brown
}

public enum Ratings
{
    Great,
    Good,
    Bad
}

public struct RatingsDialogue
{
    string[] possibleDialogues;
}

[System.Serializable]
public struct MissionInfoSO
{
    public GameObject prefab;
    public int initialWorth;
    public int secondsUntilBad;
    public int worthWhenBad;
    public string missionText;
}

public struct MissionInfoTotal
{
    public MissionInfoSO info;
    public GameObject spawnLocation;
    public GameObject dropoffLocation;
}


[CreateAssetMenu(fileName = "NewMission", menuName = "New Mission")]
public class Missions : ScriptableObject
{
    public MissionInfoSO info;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "New Dialogue SO")]
public class RatingDialogueCollection : ScriptableObject
{
    public RatingsDialogue dialogue;
}
