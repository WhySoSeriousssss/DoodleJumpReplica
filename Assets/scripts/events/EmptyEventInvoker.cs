using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EmptyEventInvoker : MonoBehaviour {

    protected Dictionary<EventName, UnityEvent> events = new Dictionary<EventName, UnityEvent>();

    public void AddListener(EventName eventName, UnityAction listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].AddListener(listener);
        }
    }

    public void RemoveListener(EventName eventName, UnityAction listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].RemoveListener(listener);
        }
    }
}
