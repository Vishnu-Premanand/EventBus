using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class EventBus<T> where T : IEvent
{

    static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

    public static void Register(IEventBinding<T> binding) => bindings.Add(binding);
    public static void DeRegister(IEventBinding<T> binding) => bindings.Remove(binding);

    public static void Raise(T @event)
    {
        foreach(var binding in bindings)
        {
            binding.OnEventArgs(@event);
            binding.OnEventNoArgs();
        }
    }
    static void Clear()
    {
        Debug.Log($"Clearing {typeof(T).Name} bindings");
        bindings.Clear();
    }
}


