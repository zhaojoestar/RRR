using UnityEngine;

public class Buff : MonoBehaviour
{
    public int BuffId;
    BuffConfig config;
    float lifeTime;
    float _lifeTime;
    float delay;
    float period;
    float _period;
    Property ppt;

    protected void Start()
    {
        config = BuffConfig.Get(BuffId);
        lifeTime = config.Duration / 1000f;
        delay = config.Delay / 1000f;
        period = config.Period / 1000f;
        ppt = GetComponent<Property>();
    }

    protected void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > lifeTime)
        {
            Destroy(this);
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
                DealEffect(true);
            }
        }
        else
        {
            DealEffect(true);
            period = lifeTime;
        }
    }

    protected void DealEffect(bool add)
    {
        var type = config.EffectType;
        var valueType = config.ValueType;
        var value = config.EffectValue;
        switch ((ENUM_EFFECT)type)
        {
            case ENUM_EFFECT.DAMAGE:
                ppt.HPChange(-value);
                break;
            case ENUM_EFFECT.HEAL:
                ppt.HPChange(value);
                break;
            case ENUM_EFFECT.ATB_CHANGE://buff
                ppt.ATBChange(valueType, value, add);
                break;
            case ENUM_EFFECT.SIZE_CHANGE://buff
                ppt.Megamorph(value, add);
                if (add)
                {
                    transform.localScale *= (1 + value / 10000);
                }
                else
                {
                    transform.localScale /= (1 + value / 10000);
                }
                break;
            case ENUM_EFFECT.BOOST://buff
                DealBoost((ENUM_BOOST)valueType, value, add);
                break;
            case ENUM_EFFECT.ABNOMALY://buff
                DealAbnomaly((ENUM_ABNOMALY)valueType, value, add);
                break;
            case ENUM_EFFECT.SHIELD://buff
                break;
            default:
                Debug.LogError("ENUM_EFFECT can not find");
                break;
        }
    }

    protected void DealBoost(ENUM_BOOST type, int value, bool add)
    {

    }

    protected void DealAbnomaly(ENUM_ABNOMALY type, int value, bool add)
    {
        var ai = GetComponent<AIBase_Monster>();
        if (!ai) return;
        switch (type)
        {
            case ENUM_ABNOMALY.VERTIGO:
                ai.OnOutOfControl(add);
                ai.OnImprison(add);
                break;
            case ENUM_ABNOMALY.IMPRISON:
                ai.OnImprison(add);
                break;
            case ENUM_ABNOMALY.THRILL:
                ai.OnOutOfControl(add);
                break;
            case ENUM_ABNOMALY.CHILL:

                break;
            case ENUM_ABNOMALY.FROZEN:
                ai.OnOutOfControl(add);
                ai.OnImprison(add);
                break;
            case ENUM_ABNOMALY.BURNING:

                break;
            case ENUM_ABNOMALY.POISONING:

                break;
            case ENUM_ABNOMALY.CHICKEN:
                ai.OnOutOfControl(add);
                break;
            case ENUM_ABNOMALY.EXPLODE:

                break;
            case ENUM_ABNOMALY.PARALYSIS:

                break;
            case ENUM_ABNOMALY.BLIND:

                break;
            case ENUM_ABNOMALY.WET:

                break;
            case ENUM_ABNOMALY.SLEEPING:
                ai.OnOutOfControl(add);
                ai.OnImprison(add);
                break;
            case ENUM_ABNOMALY.CHARMED:

                break;
            case ENUM_ABNOMALY.BLEED:

                break;
            case ENUM_ABNOMALY.CURSED:

                break;
            case ENUM_ABNOMALY.DECAYED:

                break;
            default:
                Debug.LogError("ENUM_ABNOMALY can not find");
                break;
        }
    }

    protected void OnDestroy()
    {
        DealEffect(false);
    }
}