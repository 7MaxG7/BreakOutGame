using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TEventManager<T> {
    static Dictionary<EventsEnum, List<TEventInvoker<T>>> invokers = new Dictionary<EventsEnum, List<TEventInvoker<T>>>();
    static Dictionary<EventsEnum, List<UnityAction<T>>> listeners = new Dictionary<EventsEnum, List<UnityAction<T>>>();
    public static bool Initialized { get; private set; } = false;

    public static void Initialize() {
        Initialized = true;
        foreach (EventsEnum eventName in Enum.GetValues(typeof(EventsEnum))) {
            if (invokers.ContainsKey(eventName)) {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            } else {
                invokers.Add(eventName, new List<TEventInvoker<T>>());
                listeners.Add(eventName, new List<UnityAction<T>>());
            }
        }
    }
    public static void AddInvoker(EventsEnum eventName, TEventInvoker<T> invoker_foo) {
        if (!invokers[eventName].Contains(invoker_foo)) {
            invokers[eventName].Add(invoker_foo);
            foreach (UnityAction<T> listener in listeners[eventName]) {
                invoker_foo.AddListener(eventName, listener);
            }
        }
    }
    public static void AddListener(EventsEnum eventName, UnityAction<T> listener_foo) {
        if (!listeners[eventName].Contains(listener_foo)) {
            listeners[eventName].Add(listener_foo);
            foreach (TEventInvoker<T> invoker in invokers[eventName]) {
                invoker.AddListener(eventName, listener_foo);
            }
        }
    }
    public static void RemoveInvoker(EventsEnum eventName, TEventInvoker<T> invoker_foo) {
        invokers[eventName].Remove(invoker_foo);
    }
}
