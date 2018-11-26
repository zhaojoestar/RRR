using System;
using System.Collections.Generic;

public class EventMgr
{
    public delegate void EventDelegate();
    static readonly Dictionary<ENUM_EVENT, Delegate> _delegate = new Dictionary<ENUM_EVENT, Delegate>();

    public static void AddEventListener(ENUM_EVENT e, EventDelegate c)
    {
        Delegate d;
        if (_delegate.TryGetValue(e, out d))
        {
            _delegate[e] = Delegate.Combine(d, c);
        }
        else
        {
            _delegate.Add(e, c);
        }
    }

    public static void RemoveEventListener(ENUM_EVENT e, EventDelegate c)
    {
        Delegate d;
        if (_delegate.TryGetValue(e, out d))
        {
            _delegate[e] = Delegate.Remove(d, c);
        }
        else
        {
            _delegate.Remove(e);
        }
    }

    public static void DispatchEvent(ENUM_EVENT e)
    {
        Delegate d;
        if (_delegate.TryGetValue(e, out d))
        {
            EventDelegate callback = d as EventDelegate;
            if (callback != null)
            {
                callback();
            }
        }
    }
}
