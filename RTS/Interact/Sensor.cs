using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Property Parent;

    /// <summary>
    /// 碰撞检测
    /// </summary>
    protected void OnTriggerEnter(Collider other)
    {
        if (CheckUnitType(other.gameObject))
        {
            //播放受击
            var ai = GetComponent<AIBase_Monster>();
            if (ai) ai.OnHit();
            if (Parent) MagicMgr.DealMagic(ENUM_EFFECT.DAMAGE, Parent, other.gameObject);
            //子弹处理
            Projectile bullet = gameObject.GetComponent<Projectile>();
            if (bullet)
            {
                if (bullet.Type == ENUM_PROJECTILE.NORMAL)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    protected bool CheckUnitType(GameObject obj)
    {
        var result = false;
        var ppt = obj.GetComponent<Property>();
        if (ppt && Parent)
        {
            if (ppt.Side != Parent.Side)
            {
                result = true;
            }
        }
        return result;
    }
}

