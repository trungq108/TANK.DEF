using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void OnTriggerEvent(string eventName, object eventData);
}
