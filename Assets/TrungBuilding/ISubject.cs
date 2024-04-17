using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void AddListener(IObserver observer, string eventName);
    void RemoveListener(IObserver observer, string eventName);
    void TriggerAllObservers(string eventName, object eventData);
}
