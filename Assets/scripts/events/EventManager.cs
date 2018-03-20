using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class EventManager {

    private static EventManager eventManagerInstance = null;
	public static EventManager instance
    {
        get
        {
            if (eventManagerInstance == null)
            {
                eventManagerInstance = new EventManager();
            }
            return eventManagerInstance;
        }
    }


    private static Dictionary<EventName, List<EmptyEventInvoker>> invokers = new Dictionary<EventName, List<EmptyEventInvoker>>();
    private static Dictionary<EventName, List<UnityAction>> listeners = new Dictionary<EventName, List<UnityAction>>();



    public void Initialize()
    {
        foreach (EventName eName in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(eName))
            {
                invokers.Add(eName, new List<EmptyEventInvoker>());
                listeners.Add(eName, new List<UnityAction>());
            }
            else
            {
                invokers[eName].Clear();
                listeners[eName].Clear();
            }
        }

    }


    public void AddInvoker(EventName eventName, EmptyEventInvoker emptyEventInvoker)
    {
        foreach (UnityAction listener in listeners[eventName])
        {
            emptyEventInvoker.AddListener(eventName, listener);
        }

        invokers[eventName].Add(emptyEventInvoker);
    }


    public void AddListener(EventName eventName, UnityAction listener)
    {
        foreach (EmptyEventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }

        listeners[eventName].Add(listener);
    }


    public void RemoveInvoker(EventName eventName, EmptyEventInvoker emptyEventInvoker)
    {
        foreach(UnityAction listener in listeners[eventName])
        {
            emptyEventInvoker.RemoveListener(eventName, listener);
        }
        invokers[eventName].Remove(emptyEventInvoker);
    }


    public void RemoveListener(EventName eventName, UnityAction listener)
    {
        foreach(EmptyEventInvoker invoker in invokers[eventName])
        {
            invoker.RemoveListener(eventName, listener);
        }

        listeners[eventName].Remove(listener);
    }

   
}
