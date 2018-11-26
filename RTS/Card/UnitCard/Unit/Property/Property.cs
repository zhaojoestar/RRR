using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Property : MonoBehaviour
{
    #region 属性
    public int CardID;
    public ENUM_SIDE Side;
    public ENUM_UNIT_TYPE UnitType;
    public Dictionary<ENUM_ATB, int> Attribute = new Dictionary<ENUM_ATB, int>();
    #endregion
    Dictionary<ENUM_ATB, int> AttributeBase = new Dictionary<ENUM_ATB, int>();
    float HPBase;
    Slider HPBar;

    void Start()
    {
        if (CardID != 0)
        {
            int[] atb = new int[] { };
            if (UnitType == ENUM_UNIT_TYPE.CORE)
            {
                atb = CoreConfig.Get(CardID).Attribute;
            }
            else if (UnitType == ENUM_UNIT_TYPE.OTHER)
            {
                var config = CardConfig.Get(CardID);
                atb = UnitConfig.Get(config.Value).Attribute;
            }

            for (int i = 0; i < atb.Length; i++)
            {
                Attribute[(ENUM_ATB)i] = atb[i];
                AttributeBase[(ENUM_ATB)i] = atb[i];
            }
            HPBase = AttributeBase[ENUM_ATB.HP];
        }
        var parent = GameObject.Find("Canvas/HUD").transform;
        HPBar = (Instantiate(Resources.Load(CONSTANT.CONST.RES_HUD_HPBAR), parent) as GameObject).GetComponent<Slider>();
        HPBar.fillRect.gameObject.GetComponent<Image>().color = Side == ENUM_SIDE.A ? new Color(50 / 255f, 1f, 50 / 255f) : new Color(1f, 50 / 255f, 50 / 255f);
        HPBarFollow();
        HPBarChange();
    }

    void Update()
    {
        HPBarFollow();
    }

    void HPBarFollow()
    {
        if (HPBar)
        {
            HPBar.transform.localPosition = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0, 50, 0);
        }
    }

    void HPBarChange()
    {
        if (HPBar)
        {
            HPBar.value = Attribute[ENUM_ATB.HP] / HPBase;
            if (Attribute[ENUM_ATB.HP] == 0)
            {
                Destroy(HPBar.gameObject);
            }
        }
    }

    public void HPChange(float amount)
    {
        if (amount < 0)
        {
            OnHurt();
        }
        Attribute[ENUM_ATB.HP] += (int)amount;
        if (Attribute[ENUM_ATB.HP] <= 0)
        {
            Attribute[ENUM_ATB.HP] = 0;
            //ai结算
            var ai = GetComponent<AIBase_Monster>();
            if (ai)
            {
                ai.OnDead();
            }
            //胜负结算
            if (UnitType == ENUM_UNIT_TYPE.CORE)
            {
                if (Side == ENUM_SIDE.A)
                {
                    EventMgr.DispatchEvent(ENUM_EVENT.GAMEOVER);
                }
                else if (Side == ENUM_SIDE.B)
                {
                    EventMgr.DispatchEvent(ENUM_EVENT.GAMECLEAR);
                }
            }
        }
        else if (Attribute[ENUM_ATB.HP] > HPBase)
        {
            Attribute[ENUM_ATB.HP] = (int)HPBase;
        }
        HPBarChange();
    }

    void OnHurt()
    {

    }

    public void Megamorph(int para, bool add)
    {
        if (add)
        {
            HPBase *= 1 + para / 10000;
            var hpChange = Attribute[ENUM_ATB.HP] * para / 10000;
            HPChange(hpChange);
        }
        else
        {
            HPBase /= 1 + para / 10000;
            var hpChange = -(Attribute[ENUM_ATB.HP] * para / 10000) / (1 + para / 10000);
            HPChange(hpChange);
        }
    }

    public void RestoreSize()
    {
        HPBase = AttributeBase[ENUM_ATB.HP];
    }

    public void ATBChange(int atb, int value, bool add)
    {
        if (atb == (int)ENUM_ATB.HP)
        {
            if (add)
            {
                HPBase *= 1 + value / 10000;
            }
            else
            {
                HPBase /= 1 + value / 10000;
            }
        }
        else
        {
            if (add)
            {
                Attribute[(ENUM_ATB)atb] *= (1 + value / 10000);
            }
            else
            {
                Attribute[(ENUM_ATB)atb] /= (1 + value / 10000);
            }
        }
    }
}
