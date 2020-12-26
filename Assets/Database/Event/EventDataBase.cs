using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EventDataBase",menuName ="CreateEventDataBase")]
public class EventDataBase : ScriptableObject
{
    /// <summary>
    /// イベント進行順
    /// </summary>
    [SerializeField]
    private List<Event> eventManager = new List<Event>();

    public List<Event> GetEventOrder()
    {
        return eventManager;
    }
}
