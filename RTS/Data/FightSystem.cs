using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    #region Var
    public static FightSystem Instance;

    FightUI FightU;
    public bool isFightOver { get; set; }
    public bool isVictory { get; set; }
    public List<GameObject> SideA = new List<GameObject>();
    public List<GameObject> SideB = new List<GameObject>();
    public GameObject CoreA;
    public GameObject CoreB;
    public DeckList DeckListA = new DeckList();
    public DeckList DeckListB = new DeckList();
    [HideInInspector]
    public float EnergyA = 1f, EnergyB = 0f, EnergyMaxA = 5f, EnergyMaxB = 5f, EnergyFillSpeedA = 0.5f, EnergyFillSpeedB = 2f;
    float EnergyDrawCost = 1f;
    #endregion

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetEvent(true);
        Init();
    }

    void Update()
    {
        EnergyChangeA(EnergyFillSpeedA * Time.deltaTime);
        EnergyChangeB(EnergyFillSpeedB * Time.deltaTime);
    }

    void Init()
    {
        PlayerData PD = PlayerData.Get(1);//卡组数据库
        AddCoreA(PD.Core);//设置核心
        DeckListA.Deck.AddRange(PD.Deck);//设置卡组
        FightU = FightUI.Instance;//获取UI？
        FightU.Init();
    }

    #region 卡组管理
    void EnergyChangeA(float amount)
    {
        EnergyA += amount;
        if (EnergyA < 0)
        {
            EnergyA = 0;
        }
        if (EnergyA > EnergyMaxA)
        {
            EnergyA = EnergyMaxA;
        }
        EventMgr.DispatchEvent(ENUM_EVENT.ENERGY_CHANGE);
    }

    void EnergyChangeB(float amount)
    {
        EnergyB += amount;
        if (EnergyB < 0)
        {
            EnergyB = 0;
        }
        if (EnergyB > EnergyMaxB)
        {
            EnergyB = EnergyMaxB;
        }
    }

    bool EnergyCheckA(float amount)
    {
        return EnergyA >= amount;
    }

    bool EnergyCheckB(float amount)
    {
        return EnergyB >= amount;
    }

    public void DrawA()
    {
        if (!EnergyCheckA(EnergyDrawCost)) return;
        if (DeckListA.Deck.Count == 0)
        {
            if (DeckListA.Grave.Count == 0) return;
            else
            {
                DeckListA.Deck.AddRange(RandomSortList(DeckListA.Grave));
                DeckListA.Grave.Clear();
            }
        }
        EnergyChangeA(-EnergyDrawCost);
        int add = DeckListA.Deck[0];
        DeckListA.Deck.RemoveAt(0);
        HandAddA(add);
    }

    public void DrawB()
    {
        if (!EnergyCheckB(EnergyDrawCost)) return;
        if (DeckListB.Deck.Count == 0)
        {
            if (DeckListB.Grave.Count == 0) return;
            else
            {
                DeckListB.Deck.AddRange(RandomSortList(DeckListB.Grave));
                DeckListB.Grave.Clear();
            }
        }
        EnergyChangeB(-EnergyDrawCost);
        int add = DeckListB.Deck[0];
        DeckListB.Deck.RemoveAt(0);
        HandAddB(add);
    }

    void HandAddA(int add)
    {
        DeckListA.Hand.Add(add);
        FightU.AddHand(add);
    }

    void HandAddA(List<int> add)
    {
        DeckListA.Hand.AddRange(add);
        FightU.AddHand(add);
    }

    void HandAddB(int add)
    {
        DeckListB.Hand.Add(add);
    }

    public void HandRemoveA(int remove)
    {
        DeckListA.Hand.Remove(remove);
    }

    public void HandRemoveB(int remove)
    {
        DeckListB.Hand.Remove(remove);
    }

    public void GraveAddA(int add)
    {
        DeckListA.Grave.Add(add);
        EventMgr.DispatchEvent(ENUM_EVENT.DECK_CHANGE);
    }

    void GraveAddA(List<int> add)
    {
        DeckListA.Grave.AddRange(add);
        EventMgr.DispatchEvent(ENUM_EVENT.DECK_CHANGE);
    }

    public void GraveAddB(int add)
    {
        DeckListB.Grave.Add(add);
    }

    public List<T> RandomSortList<T>(List<T> ListT)
    {
        System.Random random = new System.Random();
        List<T> newList = new List<T>();
        foreach (T item in ListT)
        {
            newList.Insert(random.Next(newList.Count + 1), item);
        }
        return newList;
    }

    public void UseCard(int id, Vector3 position)
    {
        HandRemoveA(id);
        GraveAddA(id);
        var config = CardConfig.Get(id);
        switch ((ENUM_TYPE)config.Type)
        {
            case ENUM_TYPE.UNIT:
                CreateUnitA(config.Value, position);
                break;
            case ENUM_TYPE.MAGIC:
                CreateMagic(config.Value, position);
                break;
            default:
                Debug.LogError("ENUM_TYPE can not find");
                break;
        }
    }

    void SetFightState()
    {
        isFightOver = true;
    }
    #endregion

    #region 基地管理
    /// <summary>
    /// 添加基地
    /// </summary>
    public void AddCoreA(int id)
    {
        var path = CoreConfig.Get(id).Resource;
        var parent = GameObject.Find(CONSTANT.CONST.PATH_CORE_A).transform;
        var obj = Instantiate(Resources.Load(path), parent) as GameObject;
        var ppt = obj.GetComponent<Property>();
        ppt.CardID = id;
        ppt.Side = ENUM_SIDE.A;
        ppt.UnitType = ENUM_UNIT_TYPE.CORE;
        AddSideA(obj);
        CoreA = obj;
    }

    public void AddCoreB(int id)
    {
        var path = CoreConfig.Get(id).Resource;
        var parent = GameObject.Find(CONSTANT.CONST.PATH_CORE_B).transform;
        var obj = Instantiate(Resources.Load(path), parent) as GameObject;
        var ppt = obj.GetComponent<Property>();
        ppt.CardID = id;
        ppt.Side = ENUM_SIDE.B;
        ppt.UnitType = ENUM_UNIT_TYPE.CORE;
        AddSideB(obj);
        CoreB = obj;
    }
    #endregion

    #region 单位列表管理
    public void CreateUnitA(int id, Vector3 position)
    {
        var path = UnitConfig.Get(id).Resource;
        var parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_A).transform;
        var obj = Instantiate(Resources.Load(path), position, Quaternion.Euler(new Vector3(0, 90, 0)), parent) as GameObject;
        var ppt = obj.GetComponent<Property>();
        ppt.CardID = id;
        ppt.Side = ENUM_SIDE.A;
        ppt.UnitType = ENUM_UNIT_TYPE.OTHER;
        AddSideA(obj);
    }

    public void CreateUnitB(int id)
    {
        var path = UnitConfig.Get(id).Resource;
        var parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_B).transform;
        var obj = Instantiate(Resources.Load(path), parent) as GameObject;
        obj.transform.localPosition = new Vector3(0, 0, UnityEngine.Random.Range(-10, 10));
        obj.transform.Rotate(new Vector3(0, -90, 0));
        var ppt = obj.GetComponent<Property>();
        ppt.CardID = id;
        ppt.Side = ENUM_SIDE.B;
        ppt.UnitType = ENUM_UNIT_TYPE.OTHER;
        AddSideB(obj);
    }

    public void CreateMagic(int id, Vector3 position)
    {
        var path = MagicConfig.Get(id).Resource;
        var parent = GameObject.Find(CONSTANT.CONST.PATH_BORN_C).transform;
        var obj = Instantiate(Resources.Load(path), position, Quaternion.Euler(new Vector3(0, 90, 0)), parent) as GameObject;
        var ppt = obj.GetComponent<Property>();
        ppt.CardID = id;
        ppt.Side = ENUM_SIDE.A;
        ppt.UnitType = ENUM_UNIT_TYPE.OTHER;
    }

    public void AddSideA(GameObject A)
    {
        if (A)
        {
            SideA.Add(A);
            EventMgr.DispatchEvent(ENUM_EVENT.SIDE_CHANGE);
        }
    }

    public void RemoveSideA(GameObject A)
    {
        if (A)
        {
            SideA.Remove(A);
        }
    }

    public void AddSideB(GameObject B)
    {
        if (B)
        {
            SideB.Add(B);
            EventMgr.DispatchEvent(ENUM_EVENT.SIDE_CHANGE);
        }
    }

    public void RemoveSideB(GameObject B)
    {
        if (B)
        {
            SideB.Remove(B);
        }
    }
    #endregion

    #region 消息管理
    void SetEvent(bool isAdd)
    {
        if (isAdd)
        {
            EventMgr.AddEventListener(ENUM_EVENT.GAMECLEAR, SetFightState);
            EventMgr.AddEventListener(ENUM_EVENT.GAMEOVER, SetFightState);
        }
        else
        {
            EventMgr.AddEventListener(ENUM_EVENT.GAMECLEAR, SetFightState);
            EventMgr.AddEventListener(ENUM_EVENT.GAMEOVER, SetFightState);
        }
    }

    void OnDestroy()
    {
        SetEvent(false);
    }
    #endregion
}

public class DeckList
{
    public List<int> Deck = new List<int>();
    public List<int> Hand = new List<int>();
    public List<int> Grave = new List<int>();
}
