using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public string eventName;
    [TextArea(3,10)]
    public string eventText;
    [TextArea(5, 10)]
    public string eventMouseoverText;

    public Sprite eventArt;

    [HideInInspector]public int beesCommitted;
    public int eventDifficulty;

    public EventType eventType;
    
    public GameResources succRes;
    public int succAmount;
    public GameResources failRes;
    public int failAmount;


}
