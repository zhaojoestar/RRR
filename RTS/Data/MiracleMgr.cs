using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiracleMgr : MonoBehaviour
{
    public List<ENUM_ARTIFACT> _list = new List<ENUM_ARTIFACT>();

    public void Add(ENUM_ARTIFACT m)
    {
        if (!Check(m))
        {
            _list.Add(m);
        }
    }

    public void Remove(ENUM_ARTIFACT m)
    {
        if (Check(m))
        {
            _list.Remove(m);
        }
    }

    public void Clear()
    {
        _list.Clear();
    }

    public bool Check(ENUM_ARTIFACT m)
    {
        return _list.Contains(m);
    }
}
