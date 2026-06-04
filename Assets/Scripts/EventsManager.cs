using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }

    public List<Event> genericEvents;
    public List<Event> noKingEvents;
    public List<Event> logEvents;
    public List<Event> eelEvents;
    public List<Event> heronEvents;
    public List<Event> councilEvents;
    public int daysBeforeHappeningAgain = 3; 

    private Dictionary<Event,(EventType, int)> pastEvents = new Dictionary<Event, (EventType, int)>();

    public enum EventType
    {
        Generic,
        NoKing,
        Log,
        Eel,
        Heron,
        Council
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Event GetRandomEvent()
    {
        puttingEventsBack();
        int index;
        Event randomEvent = null;
        switch (GameSceneManager.Instance.currentKing)
        {
            case KingType.None:
                index = Random.Range(0,genericEvents.Count + noKingEvents.Count);
                Debug.Log(index);
                if (index < genericEvents.Count)
                {
                    randomEvent = genericEvents[index];
                    genericEvents.RemoveAt(index);

                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Generic, GameSceneManager.Instance.currentDay));
                }
                else
                {
                    randomEvent = noKingEvents[index - genericEvents.Count];
                    noKingEvents.RemoveAt(index - genericEvents.Count);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.NoKing, GameSceneManager.Instance.currentDay));
                }
                break;

            case KingType.Log:
                index = Random.Range(0, genericEvents.Count + logEvents.Count);
                if (index < genericEvents.Count)
                {
                    randomEvent = genericEvents[index];
                    genericEvents.RemoveAt(index);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Generic, GameSceneManager.Instance.currentDay));
                }
                else
                {
                    randomEvent = logEvents[index - genericEvents.Count];
                    logEvents.RemoveAt(index - genericEvents.Count);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Log, GameSceneManager.Instance.currentDay));
                }
                break;

            case KingType.Eel:
                index = Random.Range(0, genericEvents.Count + eelEvents.Count);
                if (index < genericEvents.Count)
                {
                    randomEvent = genericEvents[index];
                    genericEvents.RemoveAt(index);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Generic, GameSceneManager.Instance.currentDay));
                }
                else
                {
                    randomEvent = eelEvents[index - genericEvents.Count];
                    eelEvents.RemoveAt(index - genericEvents.Count);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Eel, GameSceneManager.Instance.currentDay));
                }
                break;

            case KingType.Heron:
                index = Random.Range(0, heronEvents.Count);
                
                randomEvent = heronEvents[index];
                heronEvents.RemoveAt(index);
                if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Heron, GameSceneManager.Instance.currentDay));
                
                break;

            case KingType.Council:
                index = Random.Range(0, genericEvents.Count + councilEvents.Count);
                if (index < genericEvents.Count)
                {
                    randomEvent = genericEvents[index];
                    genericEvents.RemoveAt(index);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Generic, GameSceneManager.Instance.currentDay));
                }
                else
                {
                    randomEvent = councilEvents[index - genericEvents.Count];
                    councilEvents.RemoveAt(index - genericEvents.Count);
                    if (randomEvent.isRepeatable) pastEvents.Add(randomEvent, (EventType.Council, GameSceneManager.Instance.currentDay));
                }
                break;

        }
        if (randomEvent == null)
        {
            Debug.Log("Event is null");
        }
        return randomEvent;
    }

    private void puttingEventsBack()
    {
        List<Event> eventsToRemove = new List<Event>();
        foreach (var pastEvent in pastEvents)
        {
            if (GameSceneManager.Instance.currentDay - pastEvent.Value.Item2 < daysBeforeHappeningAgain)
            {
                continue;
            }

            switch (pastEvent.Value.Item1)
            {
                case EventType.Generic:
                    genericEvents.Add(pastEvent.Key);
                    break;
                case EventType.NoKing:
                    noKingEvents.Add(pastEvent.Key);
                    break;
                case EventType.Log:
                    logEvents.Add(pastEvent.Key);
                    break;
                case EventType.Eel:
                    eelEvents.Add(pastEvent.Key);
                    break;
                case EventType.Heron:
                    heronEvents.Add(pastEvent.Key);
                    break;
                case EventType.Council:
                    councilEvents.Add(pastEvent.Key);
                    break;
            }

            eventsToRemove.Add(pastEvent.Key);
        }

        foreach (var eventToRemove in eventsToRemove)
        {
            pastEvents.Remove(eventToRemove);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
