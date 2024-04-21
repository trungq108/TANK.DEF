using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectA : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Instance.TriggerEvent(EventType.OnHearing);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.Instance.TriggerEvent(EventType.OnCar);

        }
    }
}
