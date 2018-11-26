using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static void ResetObj(GameObject o)
    {
        o.transform.localPosition = Vector3.zero;
        o.transform.localRotation = Quaternion.identity;
        o.transform.localScale = Vector3.one;
    }
}
