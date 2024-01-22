using System;
using System.Collections.Generic;

public static class EventManager
{
    private static readonly Dictionary<Type, HashSet<Delegate>> EventListeners = new();

    public static void AddEventListener<T>(Action<T> listener) where T : BaseGameEvent
    {
        if (EventListeners.TryGetValue(typeof(T), out var listeners))
        {
            listeners.Add(listener);
        }
        else
        {
            EventListeners.Add(typeof(T), new HashSet<Delegate> { listener });
        }
    }
    
    public static void RemoveEventListener<T>(Action<T> listener) where T: BaseGameEvent
    {
        if (EventListeners.TryGetValue(typeof(T), out var listeners))
        {
            listeners.Remove(listener);
        }
    }
    
    public static void TriggerEvent<T>(T gameEvent) where T : BaseGameEvent
    {
        if (EventListeners.TryGetValue(gameEvent.GetType(), out var listeners))
        {
            HashSet<Delegate> invalidListeners = new ();
            foreach (var listener in listeners)
            {
                if (listener is Action<T> { Target: not null } typedListener)
                {
                    typedListener.Invoke(gameEvent);
                    continue;
                }
                
                invalidListeners.Add(listener);
            }

            foreach (var invalidListener in invalidListeners)
            {
                listeners.Remove(invalidListener);
            }
        }
    }
}