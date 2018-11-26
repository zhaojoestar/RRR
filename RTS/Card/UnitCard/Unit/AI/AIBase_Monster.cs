using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase_Monster : MonoBehaviour
{
    #region 常量
    Rigidbody _rigidbody;
    Animator _animator;
    readonly int _aniPara_near = Animator.StringToHash("Near");
    readonly int _aniPara_dead = Animator.StringToHash("Dead");
    readonly int _aniPara_skill = Animator.StringToHash("Skill");
    #endregion

    #region 变量
    ENUM_AI_STATE _STATE = ENUM_AI_STATE.NONE;
    GameObject _target = null;
    protected bool _isDead = false;
    #region Property属性
    protected Property ppt;
    protected ENUM_SIDE side;
    Dictionary<ENUM_ATB, int> atb;
    int hp = 1, atkR = 1, speed = 1, scanR = 1;
    #endregion
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (!_rigidbody)
        {
            Debug.Log(gameObject.name + "can not find rigidbody");
            return;
        }
        _animator = GetComponent<Animator>();
        if (!_animator)
        {
            Debug.Log(gameObject.name + "can not find animator");
            return;
        }
        ppt = GetComponent<Property>();
        if (!ppt)
        {
            Debug.Log(gameObject.name + "can not find property");
            return;
        }
        side = ppt.Side;
        atb = ppt.Attribute;

        SetEvent(true);
    }

    /// <summary>
    /// 读取实时数值
    /// </summary>
    void SetATB()
    {
        atkR = atb[ENUM_ATB.ATKR];
        hp = atb[ENUM_ATB.HP];
        speed = atb[ENUM_ATB.SPEED];
        scanR = atb[ENUM_ATB.SCANR];
    }

    float scanTime = 0;
    float scanPeriod = 1f;
    protected void Update()
    {
        if (FightSystem.Instance.isFightOver) return;
        if (_isDead) return;
        SetATB();
        scanTime += Time.deltaTime;
        if (_target)
        {
            if (scanTime >= scanPeriod)
            {
                scanTime = 0;
                SearchTarget();
            }
            transform.LookAt(_target.transform);
            bool isNear = Vector3.Distance(transform.position, _target.transform.position) <= atkR;
            _animator.SetBool(_aniPara_near, isNear);
            if (isNear)
            {
                StopMove();
            }
            else
            {
                Move();
            }
        }
        else
        {
            SearchTarget();
        }
    }

    /// <summary>
    /// 寻找目标
    /// </summary>
    protected void SearchTarget()
    {
        GameObject target = null;
        var dis = Mathf.Infinity;
        //遍历索敌范围内所有敌人
        List<GameObject> list = null;
        if (!FightSystem.Instance) return;
        if (side == ENUM_SIDE.A)
        {
            list = FightSystem.Instance.SideB;
        }
        else if (side == ENUM_SIDE.B)
        {
            list = FightSystem.Instance.SideA;
        }
        if (list != null && list.Count > 0)
        {
            foreach (var obj in list)
            {
                if (obj)
                {
                    var temp = transform.position - obj.transform.position;
                    var tempDis = temp.sqrMagnitude;
                    if (tempDis < dis)
                    {
                        dis = tempDis;
                        target = obj;
                    }
                }
            }
        }
        if (dis < scanR * scanR)
        {
            _target = target;
        }
        else
        {
            GameObject core = null;
            if (side == ENUM_SIDE.A)
            {
                core = FightSystem.Instance.CoreB;
            }
            else if (side == ENUM_SIDE.B)
            {
                core = FightSystem.Instance.CoreA;
            }
            if (core) _target = core;
        }
        gameObject.GetComponent<TriggerToggle>().Target = _target.transform;
    }

    /// <summary>
    /// 移动
    /// </summary>
    void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = transform.forward * speed;
        }
    }

    void StopMove()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void OnHit()
    {
        if (_rigidbody)
        {
            _rigidbody.AddForce(-transform.forward * 100f);
        }
    }

    protected void OnSkill()
    {
        _STATE = ENUM_AI_STATE.SKILL;
        _animator.SetTrigger(_aniPara_skill);
    }

    /// <summary>
    /// 死亡返回
    /// </summary>
    public void OnDead()
    {
        Debug.Log(gameObject.name + " is dead");
        _isDead = true;
        _animator.SetTrigger(_aniPara_dead);
        StopMove();
        Destroy(GetComponent<Collider>());
        if (side == ENUM_SIDE.A)
        {
            FightSystem.Instance.RemoveSideA(gameObject);
        }
        else if (side == ENUM_SIDE.B)
        {
            FightSystem.Instance.RemoveSideB(gameObject);
        }
        else
        {
            Debug.Log("Wild Monster");
        }
        StartCoroutine(OnDispose());
    }

    /// <summary>
    /// 无法控制
    /// </summary>
    public void OnOutOfControl(bool add)
    {

    }

    /// <summary>
    /// 无法移动
    /// </summary>
    public void OnImprison(bool add)
    {

    }

    /// <summary>
    /// 延时销毁
    /// </summary>
    IEnumerator OnDispose()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    #region 消息管理
    void SetEvent(bool isAdd)
    {
        if (isAdd)
        {
            EventMgr.AddEventListener(ENUM_EVENT.SIDE_CHANGE, SearchTarget);
        }
        else
        {
            EventMgr.RemoveEventListener(ENUM_EVENT.SIDE_CHANGE, SearchTarget);
        }
    }

    void OnDestroy()
    {
        SetEvent(false);
    }
    #endregion
}
