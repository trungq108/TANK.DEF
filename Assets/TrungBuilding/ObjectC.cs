using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectC : MonoBehaviour
{
    private void OnEnable()
    {
        // Đăng ký người nghe (listener) cho sự kiện "OnShoot"
        EventManager.Instance.AddListener(EventType.OnCar, ListenC);
    }

    private void OnDisable()
    {
        // Hủy đăng ký người nghe (listener) khi đối tượng bị hủy
        EventManager.Instance.RemoveListener(EventType.OnCar, ListenC);
    }

    void ListenC()
    {
        Debug.Log("We On Car");
    }
}