using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEventInvoker<T> : MonoBehaviour {
    protected Dictionary<EventsEnum, UnityEvent<T>> events = new Dictionary<EventsEnum, UnityEvent<T>>();

    public void AddListener(EventsEnum eventName, UnityAction<T> listener_foo) {
        if (events.ContainsKey(eventName)) {
            events[eventName].AddListener(listener_foo);
        }
    }
}
