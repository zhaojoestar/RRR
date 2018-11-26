using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Sensor
{
    public int MagicId;
    public GameObject Target;
    MagicConfig config;
    float lifeTime;
    float _lifeTime;
    float delay;
    float period;
    float _period;
    bool isTrigger;

    void Start()
    {
        config = MagicConfig.Get(MagicId);
        lifeTime = config.Duration / 1000f;
        delay = config.Delay / 1000f;
        period = config.Period / 1000f;
    }

    void Update()
    {
        isTrigger = false;
        _lifeTime += Time.deltaTime;
        if (_lifeTime > lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        if (_lifeTime < delay) return;
        if (period > 0)
        {
            _period += Time.deltaTime;
            if (_period > period)
            {
                _period = 0;
                //周期触发
                isTrigger = true;
                DealEffect();
            }
        }
        else
        {
            period = lifeTime;
            isTrigger = true;
            DealEffect();
        }
    }

    void DealEffect()
    {
        if (Target != null)
        {
            MagicMgr.DealMagic((ENUM_EFFECT)config.EffectType, config.EffectValue, Target);
        }
    }

    new void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (isTrigger)
        {
            if (CheckSide(other.gameObject))
            {
                MagicMgr.DealMagic((ENUM_EFFECT)config.EffectType, config.EffectValue, other.gameObject);
            }
        }
    }

    bool CheckSide(GameObject o)
    {
        bool result = false;
        var ppt = o.GetComponent<Property>();
        if (ppt && Parent)
        {
            switch ((ENUM_TARGET)config.SideType)
            {
                case ENUM_TARGET.ANY:
                    result = true;
                    break;
                case ENUM_TARGET.OTHER:
                    result = ppt.Side != Parent.Side;
                    break;
                case ENUM_TARGET.SAME:
                    result = ppt.Side == Parent.Side;
                    break;
                default:
                    Debug.LogError("ENUM_TARGET can not find");
                    break;
            }
        }
        return result;
    }
}
