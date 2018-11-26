using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSystem : MonoBehaviour
{
    public static MapSystem Instance;
    MapUI MapU;
    public PlayerData PlayerD;
    public MapData[] RowData;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayerD = PlayerData.Get(1);
        MapU = MapUI.Instance;
        MapU.Init();
    }

    public bool DealMapEvent(int type)
    {
        var result = true;
        switch ((ENUM_MAP_EVENT)type)
        {
            case ENUM_MAP_EVENT.NONE:
                result = false;
                break;
            case ENUM_MAP_EVENT.EMPTY:
                break;
            case ENUM_MAP_EVENT.UNKNOWN:
                MapU.Deal_Unknown();
                break;
            case ENUM_MAP_EVENT.BONFIRE:
                MapU.Deal_Bonfire();
                break;
            case ENUM_MAP_EVENT.SHOP:
                MapU.Deal_Shop();
                break;
            case ENUM_MAP_EVENT.TREASURE:
                MapU.Deal_Treasure();
                break;
            case ENUM_MAP_EVENT.BATTLE:
                SceneManager.LoadScene((int)ENUM_SCENE.FIELD);
                break;
            case ENUM_MAP_EVENT.BATTLE_ELITE:
                SceneManager.LoadScene((int)ENUM_SCENE.FIELD);
                break;
            case ENUM_MAP_EVENT.BATTLE_BOSS:
                SceneManager.LoadScene((int)ENUM_SCENE.FIELD);
                break;
            default:
                Debug.LogError("ENUM_MAP_EVENT can not find");
                break;
        }
        return result;
    }
}
