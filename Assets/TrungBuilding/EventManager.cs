using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    OnShoot,
    OnHit,
    OnGameOver,
    OnCar,
    Spawm,
    OnHearing,


    // Thêm các loại sự kiện khác tại đây nếu cần thiết
}
public class EventManager : MonoBehaviour
{
    // Định nghĩa một delegate cho các sự kiện không có tham số
    public delegate void EventDelegate();

    // Dictionary lưu trữ các người nghe (listeners) cho từng loại sự kiện
    private Dictionary<EventType, EventDelegate> eventDictionary = new Dictionary<EventType, EventDelegate>();

    // Singleton pattern để có thể truy cập EventManager từ bất kỳ đâu
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("EventManager");
                instance = go.AddComponent<EventManager>();
            }
            return instance;
        }
    }

    // Đăng ký người nghe (listener) cho một loại sự kiện
    public void AddListener(EventType eventType, EventDelegate listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = null;
        }
        eventDictionary[eventType] += listener;
    }

    // Hủy đăng ký người nghe (listener) cho một loại sự kiện
    public void RemoveListener(EventType eventType, EventDelegate listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
        }
    }

    // Gửi sự kiện (trigger event) cho một loại sự kiện
    public void TriggerEvent(EventType eventType)
    {
        EventDelegate eventDelegate;
        if (eventDictionary.TryGetValue(eventType, out eventDelegate))
        {
            // Gọi tất cả các người nghe của loại sự kiện này
            eventDelegate?.Invoke();
        }
    }
}
