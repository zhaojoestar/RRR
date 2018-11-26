using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public Property Parent;
    public GameObject Projectile;

    public void Emit(Transform target)
    {
        var obj = Instantiate(Projectile, transform) as GameObject;
        obj.GetComponent<Sensor>().Parent = Parent;
        obj.GetComponent<Projectile>().Target = target;
        obj.transform.parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
    }
}
