using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardID : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int cardID;
    GameObject prefab;

    void Start()
    {
        //自动触发
    }

    void OnHand()
    {

    }

    void UseCard()
    {
        FightSystem.Instance.UseCard(cardID, prefab.transform.position);
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Start");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        var image = gameObject.GetComponent<Image>();
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer(CONSTANT.CONST.LAYER_GROUND)))
        {
            image.enabled = false;
            if (prefab)
            {
                prefab.SetActive(true);
                prefab.transform.position = hit.point;
            }
            else
            {
                var config = CardConfig.Get(cardID);
                switch ((ENUM_TYPE)config.Type)
                {
                    case ENUM_TYPE.UNIT:
                        var path = UnitConfig.Get(config.Value).Resource;
                        prefab = Instantiate(Resources.Load(path), GameObject.Find(CONSTANT.CONST.PATH_AREA_A).transform) as GameObject;
                        foreach (var component in prefab.GetComponents<Component>())
                        {
                            if (!(component is Transform))
                            {
                                Destroy(component);
                            }
                        }
                        prefab.transform.position = hit.point;
                        prefab.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case ENUM_TYPE.MAGIC:
                        var path2 = MagicConfig.Get(config.Value).Resource;
                        prefab = Instantiate(Resources.Load(path2), GameObject.Find(CONSTANT.CONST.PATH_AREA_A).transform) as GameObject;
                        prefab.transform.position = hit.point;
                        break;
                    default:
                        Debug.LogError("ENUM_TYPE can not find");
                        break;
                }
            }
        }
        else
        {
            image.enabled = true;
            if (prefab)
            {
                prefab.SetActive(false);
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer(CONSTANT.CONST.LAYER_GROUND)))
        {
            UseCard();
        }
        else
        {
            //重设位置
        }
    }

    void OnDestroy()
    {
        if (prefab)
        {
            Destroy(prefab);
        }
    }
}
