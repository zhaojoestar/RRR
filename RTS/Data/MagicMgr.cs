using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicMgr : MonoBehaviour
{
    public static void Init(MagicConfig config, Transform parent, Property ppt)
    {
        //确定目标(可能多个)
        var targets = SearchTarget(config, parent, ppt);
        //连接方式(没有目标)
        if ((ENUM_RANGE)config.RangeType == ENUM_RANGE.COLLIDER)
        {
            var obj = Instantiate(Resources.Load(config.Resource), parent) as GameObject;
            var magic = obj.GetComponent<Magic>();
            magic.MagicId = config.ID;
            magic.Parent = ppt;
            obj.transform.parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
        }
        else
        {
            foreach (var target in targets)
            {
                //加载特效
                var obj = Instantiate(Resources.Load(config.Resource)) as GameObject;
                var magic = obj.GetComponent<Magic>();
                magic.MagicId = config.ID;
                magic.Parent = ppt;
                magic.Target = target;
                switch ((ENUM_CONNECT)config.Connect)
                {
                    case ENUM_CONNECT.AIR:
                        obj.transform.parent = target.transform;
                        Util.ResetObj(obj);
                        break;
                    case ENUM_CONNECT.PROJECTILE:
                        obj.transform.parent = parent.transform;
                        Util.ResetObj(obj);
                        break;
                    case ENUM_CONNECT.LASER:
                        obj.transform.parent = parent.transform;
                        Util.ResetObj(obj);
                        break;
                    case ENUM_CONNECT.LINE:
                        obj.transform.parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
                        Util.ResetObj(obj);
                        var line = obj.GetComponent<UVChainLightning>();
                        line.start = parent;
                        line.target = target.transform;
                        break;
                    case ENUM_CONNECT.CHAIN:
                        obj.transform.parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
                        Util.ResetObj(obj);
                        var chain = obj.GetComponent<UVChainLightning>();
                        chain.start = parent;
                        chain.target = target.transform;
                        break;
                    case ENUM_CONNECT.PRISM:
                        obj.transform.parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
                        Util.ResetObj(obj);
                        var prism = obj.GetComponent<UVChainLightning>();
                        prism.start = parent;
                        prism.target = target.transform;
                        break;
                    default:
                        Debug.LogError("ENUM_CONNECT can not find");
                        break;
                }
            }
        }
    }

    static List<GameObject> SearchTarget(MagicConfig config, Transform parent, Property ppt)
    {
        List<GameObject> result = new List<GameObject>();
        //遍历所有
        List<GameObject> list = null;
        if (!FightSystem.Instance) return null;
        switch ((ENUM_TARGET)config.SideType)
        {
            case ENUM_TARGET.ANY:
                break;
            case ENUM_TARGET.OTHER:
                if (ppt.Side == ENUM_SIDE.A)
                {
                    list = FightSystem.Instance.SideB;
                }
                else if (ppt.Side == ENUM_SIDE.B)
                {
                    list = FightSystem.Instance.SideA;
                }
                break;
            case ENUM_TARGET.SAME:
                if (ppt.Side == ENUM_SIDE.A)
                {
                    list = FightSystem.Instance.SideA;
                }
                else if (ppt.Side == ENUM_SIDE.B)
                {
                    list = FightSystem.Instance.SideB;
                }
                break;
            default:
                Debug.LogError("ENUM_TARGET can not find");
                break;
        }
        //范围
        switch ((ENUM_RANGE)config.RangeType)
        {
            case ENUM_RANGE.SINGLE:
                GameObject temp = null;
                var dis = Mathf.Infinity;
                if (list != null && list.Count > 0)
                {
                    foreach (var obj in list)
                    {
                        if (obj)
                        {
                            if (obj != parent.gameObject)
                            {
                                var _temp = parent.position - obj.transform.position;
                                var tempDis = _temp.sqrMagnitude;
                                if (tempDis < dis)
                                {
                                    dis = tempDis;
                                    temp = obj;
                                }
                            }
                        }
                    }
                }
                result.Add(temp);
                break;
            case ENUM_RANGE.COLLIDER:
                break;
            case ENUM_RANGE.ALL:
                result.AddRange(list);
                if (result.Contains(parent.gameObject))
                {
                    result.Remove(parent.gameObject);
                }
                break;
            default:
                Debug.LogError("ENUM_RANGE can not find");
                break;
        }
        return result;
    }

    /// <summary>
    /// 接触处理，effect类型分析
    /// </summary>
    public static void DealMagic(ENUM_EFFECT Effect, Property Sender, GameObject Recevier)
    {
        var ATB = Sender.Attribute;
        switch (Effect)
        {
            case ENUM_EFFECT.DAMAGE:
                var dam = ATB[ENUM_ATB.ATK];
                Recevier.GetComponent<Property>().HPChange(-dam);
                break;
            case ENUM_EFFECT.HEAL:
                var heal = ATB[ENUM_ATB.ATK];
                Recevier.GetComponent<Property>().HPChange(heal);
                break;
            default:
                Debug.LogError("ENUM_EFFECT can not find");
                break;
        }
    }

    public static void DealMagic(ENUM_EFFECT Effect, int Value, GameObject Recevier)
    {

        switch (Effect)
        {
            case ENUM_EFFECT.DAMAGE:
                Recevier.GetComponent<Property>().HPChange(-Value);
                break;
            case ENUM_EFFECT.HEAL:
                Recevier.GetComponent<Property>().HPChange(Value);
                break;
            case ENUM_EFFECT.ATB_CHANGE://恢复
                AddBuff(Value, Recevier);
                break;
            case ENUM_EFFECT.SIZE_CHANGE://恢复
                AddBuff(Value, Recevier);
                break;
            case ENUM_EFFECT.BOOST://恢复
                AddBuff(Value, Recevier);
                break;
            case ENUM_EFFECT.ABNOMALY://恢复
                AddBuff(Value, Recevier);
                break;
            case ENUM_EFFECT.SHIELD://恢复
                AddBuff(Value, Recevier);
                break;
            default:
                Debug.LogError("ENUM_EFFECT can not find");
                break;
        }
    }

    static void AddBuff(int buffId, GameObject o)
    {
        var buffC = BuffConfig.Get(buffId);
        var type = CONSTANT.CONST.BUFF_PERFIX + Enum.GetName(typeof(ENUM_EFFECT), buffC.EffectType);
        var buff = o.AddComponent(Type.GetType(type)) as Buff;
        buff.BuffId = buffId;
    }
}
