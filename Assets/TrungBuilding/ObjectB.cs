using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectB : MonoBehaviour
{
    private void OnEnable()
    {
        // Đăng ký người nghe (listener) cho sự kiện "OnShoot"
        EventManager.Instance.AddListener(EventType.OnHearing, ListenB);
    }

    private void OnDisable()
    {
        // Hủy đăng ký người nghe (listener) khi đối tượng bị hủy
        EventManager.Instance.RemoveListener(EventType.OnHearing, ListenB);
    }

    void ListenB()
    {
        Debug.Log("We Are Hearing");
    }
}