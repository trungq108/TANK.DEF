using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObservableManager : ISubject
{
    private static ObservableManager instance;
    public static ObservableManager Instance => instance;

    private Dictionary<string, List<IObserver>> observers = new Dictionary<string, List<IObserver>>();

    public void AddListener(IObserver observer, string eventName)
    {
        if (!observers.ContainsKey(eventName))
        {
            observers[eventName] = new List<IObserver>();
        }

        observers[eventName].Add(observer);
    }
    
    public void RemoveListener(IObserver observer, string eventName)
    {
        if (observers.ContainsKey(eventName))
        {
            observers[eventName].Remove(observer);
        }
    }

    public void TriggerAllObservers(string eventName, object eventData)
    {
        if (observers.ContainsKey(eventName))
        {
            foreach (IObserver obs in observers[eventName])
            {
                obs.OnTriggerEvent(eventName, eventData);
            }
        }
    }
}
