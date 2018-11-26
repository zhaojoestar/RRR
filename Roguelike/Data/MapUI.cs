using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    #region Var
    public static MapUI Instance;
    MapSystem MapS;
    int _row = 1, _column = 0, _showRow = 3;
    const int SIBLING_COUNT = 5;
    int _sibling = 2;
    float _moveDis = 350f;
    #region UI
    Transform _canvas, _map, _deckMask;
    Button _deckBtn, _deckClose, _ME_Unknown_Close, _ME_Bonfire_Close, _ME_Shop_Close, _ME_Treasure_Close;
    Text _mapName, _deckNum, _levelNum, _hpNum, _coinNum;
    GameObject _left, _right, _deck, _ME, _ME_Unknown, _ME_Bonfire, _ME_Shop, _ME_Treasure;
    Slider _hpSlider;
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
        _mapName = _canvas.Find("PathName/Text").gameObject.GetComponent<Text>();
        _map = _canvas.Find("Path/List");
        _left = _canvas.Find("Path/Left").gameObject;
        _right = _canvas.Find("Path/Right").gameObject;
        _deckBtn = _canvas.Find("Panel/Deck").gameObject.GetComponent<Button>();
        _deckNum = _canvas.Find("Panel/Deck/Num/Text").gameObject.GetComponent<Text>();
        _levelNum = _canvas.Find("Panel/Level").gameObject.GetComponent<Text>();
        _hpSlider = _canvas.Find("Panel/Slider").gameObject.GetComponent<Slider>();
        _hpNum = _canvas.Find("Panel/Slider/Text").gameObject.GetComponent<Text>();
        _coinNum = _canvas.Find("Panel/Coin/Text").gameObject.GetComponent<Text>();
        _deck = _canvas.Find("Deck").gameObject;
        _deckMask = _canvas.Find("Deck/Mask");
        _deckClose = _canvas.Find("Deck/Close").gameObject.GetComponent<Button>();
        _ME = _canvas.Find("MapEvent").gameObject;
        _ME_Unknown = _canvas.Find("MapEvent/Unknown").gameObject;
        _ME_Unknown_Close = _canvas.Find("MapEvent/Unknown/Close").gameObject.GetComponent<Button>();
        _ME_Bonfire = _canvas.Find("MapEvent/Bonfire").gameObject;
        _ME_Bonfire_Close = _canvas.Find("MapEvent/Bonfire").gameObject.GetComponent<Button>();
        _ME_Shop = _canvas.Find("MapEvent/Shop").gameObject;
        _ME_Shop_Close = _canvas.Find("MapEvent/Shop/Close").gameObject.GetComponent<Button>();
        _ME_Treasure = _canvas.Find("MapEvent/Treasure").gameObject;
        _ME_Treasure_Close = _canvas.Find("MapEvent/Treasure").gameObject.GetComponent<Button>();

        _deckBtn.onClick.AddListener(ShowDeck);
        _deckClose.onClick.AddListener(() => _deck.SetActive(false));
        _ME_Unknown_Close.onClick.AddListener(() => { _ME_Unknown.SetActive(false); MapEventCallBack(); });
        _ME_Bonfire_Close.onClick.AddListener(() => { _ME_Bonfire.SetActive(false); MapEventCallBack(); });
        _ME_Shop_Close.onClick.AddListener(() => { _ME_Shop.SetActive(false); MapEventCallBack(); });
        _ME_Treasure_Close.onClick.AddListener(() => { _ME_Treasure.SetActive(false); MapEventCallBack(); });

        SetEvent(true);
    }

    public void Init()
    {
        MapS = MapSystem.Instance;
        MapChange();
        DeckChange();
        PlayerLevelChange();
    }

    void MapChange()
    {
        if (_map.childCount < _showRow)
        {
            var count = _showRow - _map.childCount;
            for (int i = 0; i < count; i++)
            {
                Instantiate(Resources.Load(CONSTANT.CONST.RES_HUD_ROW), _map);
            }
        }
        for (int i = 0; i < _showRow; i++)
        {
            //添加行
            var rowData = MapData.GetRowData(GetNextRow(i));
            var row = _map.GetChild(i);
            row.SetAsFirstSibling();
            //设置行
            for (int t = 0; t < CONSTANT.CONST.MAP_LENGTH; t++)
            {
                var eventId = rowData[t].EventType;
                var me = MapEventConfig.Get(eventId);
                var gate = row.GetChild(t).gameObject;
                gate.SetActive(true);
                gate.gameObject.GetComponent<Image>().sprite = Resources.Load(me.Texture, typeof(Sprite)) as Sprite;
                var btn = gate.gameObject.GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => MapClick(gate));
            }
        }
    }

    public void MapClick(GameObject e)
    {
        if (e.transform.parent.GetSiblingIndex() == (_showRow - 1))
        {
            _column = e.transform.GetSiblingIndex();
            MapS.RowData = MapData.GetRowData(_row);
            if (MapS.DealMapEvent(MapS.RowData[_column].EventType))
            {
                for (int i = 0; i < e.transform.parent.childCount; i++)
                {
                    e.transform.parent.GetChild(i).gameObject.SetActive(i == _column);
                }
                if (_column < _sibling && _sibling > 0)
                {
                    _sibling--;
                    _map.transform.Translate(new Vector3(_moveDis, 0, 0));
                }
                else if (_column > _sibling && _sibling < SIBLING_COUNT - 1)
                {
                    _sibling++;
                    _map.transform.Translate(new Vector3(-_moveDis, 0, 0));
                }
                _left.SetActive(_sibling > 0);
                _right.SetActive(_sibling < SIBLING_COUNT - 1);
                _row = GetNextRow(1);//下一行
            }
        }
    }

    #region MapEvent处理
    public void Deal_Unknown()
    {
        _ME_Unknown.SetActive(true);
    }

    public void Deal_Bonfire()
    {
        _ME_Bonfire.SetActive(true);
    }

    public void Deal_Shop()
    {
        _ME_Shop.SetActive(true);
    }

    public void Deal_Treasure()
    {
        _ME_Treasure.SetActive(true);
    }
    #endregion

    void MapEventCallBack()
    {
        StartCoroutine(ShowNextRow());
    }

    IEnumerator ShowNextRow()
    {
        yield return new WaitForSeconds(0.5f);
        MapChange();
    }

    int GetNextRow(int i)
    {
        var tempRow = _row + i;
        if (tempRow > MapData.RowCount)
        {
            tempRow -= MapData.RowCount;
        }
        return tempRow;
    }

    void DeckChange()
    {
        _deckNum.text = MapS.PlayerD.Deck.Count.ToString();
    }

    void ShowDeck()
    {
        _deck.SetActive(true);
        var add = MapS.PlayerD.Deck;
        if (add.Count > _deckMask.childCount)
        {
            var count = add.Count - _deckMask.childCount;
            for (int i = 0; i < count; i++)
            {
                Instantiate(Resources.Load(CONSTANT.CONST.RES_HUD_CARD), _deckMask);
            }
        }
        for (int i = 0; i < _deckMask.childCount; i++)
        {
            var card = _deckMask.GetChild(i).gameObject;
            if (i < add.Count)
            {
                card.SetActive(true);
                card.GetComponent<CardID>().cardID = add[i];
                card.GetComponent<Image>().sprite = Resources.Load(CardConfig.Get(add[i]).Texture, typeof(Sprite)) as Sprite;
                card.GetComponent<Image>().raycastTarget = false;//屏蔽CardID点击事件
            }
            else
            {
                card.SetActive(false);
            }
        }
    }

    void PlayerLevelChange()
    {
        _levelNum.text = "Lv." + MapS.PlayerD.Core.ToString();
    }

    #region 消息管理
    void SetEvent(bool isAdd)
    {
        if (isAdd)
        {
            EventMgr.AddEventListener(ENUM_EVENT.ME_CALLBACK, MapEventCallBack);
        }
        else
        {
            EventMgr.RemoveEventListener(ENUM_EVENT.ME_CALLBACK, MapEventCallBack);
        }
    }

    void OnDestroy()
    {
        SetEvent(false);
    }
    #endregion
}
