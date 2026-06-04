using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Scriptable Objects/Event")]
public class Event : ScriptableObject
{
    public bool isRepeatable = true;

    public Line[] lines;
    public Sprite leftPotrait;
    public Sprite rightPotrait;

    public Choice yesChoice;
    public Choice noChoice;
}

[System.Serializable]
public class Line
{
    public string line;
    public bool isLeftSpeaking = false;
    public bool isRightSpeaking = false;
}


[System.Serializable]
public class Choice
{
    public List<StatChange> statChanges;
    public Line[] afterChoiceLine;
}

[System.Serializable]
public class StatChange
{
    public StatType stat;
    public int amount;
}

public enum StatType
{
    Stability,
    Discontent,
    Favor,
    Population
}

