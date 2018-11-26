using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    #region Var
    public static FightUI Instance;
    FightSystem FightS;
    #region UI
    Transform _canvas;
    Slider EnergyGage, EnergyGageFixed;
    Text EnergyGageValue, DeckNum, GraveNum, resultText;
    Button DrawBtn, _resultBtn;
    GameObject _result;
    #endregion
    #endregion

    void Awake()
    {
        Instance = this;
        GetUI();
    }

    void GetUI()
    {
        _canvas = GameObject.Find("Canvas").transform;
        EnergyGage = _canvas.Find("Table/Energy/Slider").GetComponent<Slider>();
        EnergyGageFixed = _canvas.Find("Table/Energy/Fixed").GetComponent<Slider>();
        EnergyGageValue = _canvas.Find("Table/Energy/Text").GetComponent<Text>();
        DeckNum = _canvas.Find("Table/Deck/Num/Text").GetComponent<Text>();
        GraveNum = _canvas.Find("Table/Grave/Num/Text").GetComponent<Text>();
        DrawBtn = _canvas.Find("Table/Draw").GetComponent<Button>();
        _result = _canvas.Find("Result").gameObject;
        resultText = _canvas.Find("Result/Text").gameObject.GetComponent<Text>();
        _resultBtn = _canvas.Find("Result/Button").gameObject.GetComponent<Button>();

        DrawBtn.onClick.AddListener(DrawA);
        SetEvent(true);
    }

    public void Init()
    {
        FightS = FightSystem.Instance;//获取数据
        SetDeck();//设置卡组和墓地
        AddHand(FightS.DeckListA.Hand);//设置手牌
    }

    public void EnergyChange()
    {
        var EnergyA = FightS.EnergyA;
        var EnergyMaxA = FightS.EnergyMaxA;
        var EnergyAFixed = Mathf.FloorToInt(EnergyA);

        EnergyGage.value = EnergyA / EnergyMaxA;
        EnergyGageValue.text = EnergyAFixed.ToString();
        EnergyGageFixed.value = EnergyAFixed / EnergyMaxA;
    }

    void SetDeck()
    {
        DeckNum.text = FightS.DeckListA.Deck.Count.ToString();
        GraveNum.text = FightS.DeckListA.Grave.Count.ToString();
    }

    public void AddHand(List<int> add)
    {
        for (int i = 0; i < add.Count; i++)
        {
            AddHand(add[i]);
        }
    }

    public void AddHand(int add)
    {
        GameObject card = Instantiate(Resources.Load(CONSTANT.CONST.RES_HUD_CARD), GameObject.Find("Hand").transform) as GameObject;
        card.GetComponent<CardID>().cardID = add;
        card.GetComponent<Image>().sprite = Resources.Load(CardConfig.Get(add).Texture, typeof(Sprite)) as Sprite;
    }

    void DrawA()
    {
        FightS.DrawA();
        SetDeck();
    }

    void ShowWin()
    {
        _result.SetActive(true);
        resultText.text = "Victory";
        _resultBtn.onClick.AddListener(() => SceneManager.LoadScene((int)ENUM_SCENE.MAP));
    }

    void ShowFail()
    {
        _result.SetActive(true);
        resultText.text = "Fail";
        _resultBtn.onClick.AddListener(() => SceneManager.LoadScene((int)ENUM_SCENE.MAP));
    }

    #region 消息管理
    void SetEvent(bool isAdd)
    {
        if (isAdd)
        {
            EventMgr.AddEventListener(ENUM_EVENT.DECK_CHANGE, SetDeck);
            EventMgr.AddEventListener(ENUM_EVENT.GAMECLEAR, ShowWin);
            EventMgr.AddEventListener(ENUM_EVENT.GAMEOVER, ShowFail);
            EventMgr.AddEventListener(ENUM_EVENT.ENERGY_CHANGE, EnergyChange);
        }
        else
        {
            EventMgr.RemoveEventListener(ENUM_EVENT.DECK_CHANGE, SetDeck);
            EventMgr.RemoveEventListener(ENUM_EVENT.GAMECLEAR, ShowWin);
            EventMgr.RemoveEventListener(ENUM_EVENT.GAMEOVER, ShowFail);
            EventMgr.RemoveEventListener(ENUM_EVENT.ENERGY_CHANGE, EnergyChange);
        }
    }

    void OnDestroy()
    {
        SetEvent(false);
    }
    #endregion
}
