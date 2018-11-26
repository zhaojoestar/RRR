#region 常量
namespace CONSTANT
{
    /// <summary>
    /// 常量
    /// </summary>
    public class CONST
    {
        #region Para参数
        /// <summary>
        /// 一行关卡数
        /// </summary>
        public const int MAP_LENGTH = 5;
        #endregion
        #region string字符串
        public const string PATH_AREA_A = "Map/area/A";
        public const string PATH_AREA_B = "Map/area/B";
        public const string PATH_CORE_A = "A/CoreA";
        public const string PATH_CORE_B = "B/CoreB";
        public const string PATH_BORN_A = "A/BornA";
        public const string PATH_BORN_B = "B/BornB";
        public const string PATH_BORN_C = "C";
        public const string RES_HUD_CARD = "Prefabs/HUD/Card";
        public const string RES_HUD_ROW = "Prefabs/HUD/Row";
        public const string RES_HUD_HPBAR = "Prefabs/HUD/HPBar";
        public const string LAYER_GROUND = "Ground";
        public const string LAYER_UNIT = "Unit";
        public const string BUFF_PERFIX = "BUFF_";
        #endregion
    }
}
#endregion

// 枚举
#region RTS

#region DECK卡组
/// <summary>
/// Locations区域
/// </summary>
public enum ENUM_LOCATION
{
    /// <summary>
    /// 卡组
    /// </summary>
    DECK,
    /// <summary>
    /// 手牌
    /// </summary>
    HAND,
    /// <summary>
    /// 怪兽区
    /// </summary>
    MZONE,
    /// <summary>
    /// 墓地
    /// </summary>
    GRAVE,
    /// <summary>
    /// 除外区
    /// </summary>
    REMOVED,
}

/// <summary>
/// Type卡牌类型
/// </summary>
public enum ENUM_TYPE
{
    /// <summary>
    /// 单位
    /// </summary>
    UNIT,
    /// <summary>
    /// 魔法
    /// </summary>
    MAGIC,
    /// <summary>
    /// 奇迹
    /// </summary>
    MIRACLE,
    /// <summary>
    /// 负面
    /// </summary>
    NEGATIVE,
}
#endregion

#region Field战场
/// <summary>
/// 阵营
/// </summary>
public enum ENUM_SIDE
{
    A,
    B,
    C,
}

/// <summary>
/// UnitType单位类型
/// </summary>
public enum ENUM_UNIT_TYPE
{
    /// <summary>
    /// 基地
    /// </summary>
    CORE,
    /// <summary>
    /// 其他
    /// </summary>
    OTHER,
}

/// <summary>
/// Projectile发射物类型
/// </summary>
public enum ENUM_PROJECTILE
{
    /// <summary>
    /// 普通发射物-子弹
    /// </summary>
    NORMAL,
    /// <summary>
    /// 穿透发射物
    /// </summary>
    PIERCING,
    /// <summary>
    /// 散弹(多子弹，随机落点)
    /// </summary>
    SCATTER,
    /// <summary>
    /// 粘弹-延迟弹
    /// </summary>
    GLUE,
    /// <summary>
    /// 穿透发射物
    /// </summary>
    HOMING,
    /// <summary>
    /// 抛射物
    /// </summary>
    PARABOLIC,
    /// <summary>
    /// 弹跳
    /// </summary>
    BOUNCE,
    /// <summary>
    /// 链接
    /// </summary>
    CHAIN,
    /// <summary>
    /// 雪球
    /// </summary>
    SNOWBALL,
}

/// <summary>
/// RACE种族
/// </summary>
public enum ENUM_RACE
{
    /// <summary>
    /// 人类
    /// </summary>
    HUMAN,
    /// <summary>
    /// 有翼族
    /// </summary>
    WINGED,
}

/// <summary>
/// 基础属性
/// </summary>
public enum ENUM_ATB
{
    /// <summary>
    /// 攻击力
    /// </summary>
    ATK,
    /// <summary>
    /// 血量
    /// </summary>
    HP,
    /// <summary>
    /// 攻击范围
    /// </summary>
    ATKR,
    /// <summary>
    /// 移动速度
    /// </summary>
    SPEED,
    /// <summary>
    /// 索敌范围
    /// </summary>
    SCANR,
}

/// <summary>
/// AI状态
/// </summary>
public enum ENUM_AI_STATE
{
    /// <summary>
    /// 无(动画停滞)
    /// </summary>
    NONE,
    /// <summary>
    /// 移动
    /// </summary>
    MOVE,
    /// <summary>
    /// 攻击
    /// </summary>
    ATTACK,
    /// <summary>
    /// 死亡
    /// </summary>
    DEAD,
    /// <summary>
    /// 技能
    /// </summary>
    SKILL,
}

/// <summary>
/// Feature特性
/// </summary>
public enum ENUM_FEATURE
{
}

/// <summary>
/// 元素
/// </summary>
public enum ENUM_ELEMENT
{
    /// <summary>
    /// 水-二十面体
    /// </summary>
    WATER,
    /// <summary>
    /// 气-八面体
    /// </summary>
    AIR,
    /// <summary>
    /// 火-四面体
    /// </summary>
    FIRE,
    /// <summary>
    /// 土-六面体
    /// </summary>
    EARTH
}
#endregion

#region SKILL技能
/// <summary>
/// 目标阵营
/// </summary>
public enum ENUM_TARGET
{
    /// <summary>
    /// 任意
    /// </summary>
    ANY,
    /// <summary>
    /// 敌对方
    /// </summary>
    OTHER,
    /// <summary>
    /// 已方
    /// </summary>
    SAME,
}

/// <summary>
/// 技能范围
/// </summary>
public enum ENUM_RANGE
{
    /// <summary>
    /// 单体(需要目标和范围)
    /// </summary>
    SINGLE,
    /// <summary>
    /// 多个(需要目标和范围)
    /// </summary>
    MULTI,
    /// <summary>
    /// 碰撞体
    /// </summary>
    COLLIDER,
    /// <summary>
    /// 全体
    /// </summary>
    ALL,
    /// <summary>
    /// 随机单个
    /// </summary>
    RANDOM,
}

/// <summary>
/// 连接方式
/// </summary>
public enum ENUM_CONNECT
{
    /// <summary>
    /// 无(凭空)
    /// </summary>
    AIR,
    /// <summary>
    /// 子弹
    /// </summary>
    PROJECTILE,
    /// <summary>
    /// 射线
    /// </summary>
    LASER,
    /// <summary>
    /// 连线
    /// </summary>
    LINE,
    /// <summary>
    /// 闪电链
    /// </summary>
    CHAIN,
    /// <summary>
    /// 光棱射线
    /// </summary>
    PRISM,
}

/// <summary>
/// 作用次数
/// </summary>
public enum ENUM_FREQUNCE
{
    /// <summary>
    /// 单次
    /// </summary>
    ONCE,
    /// <summary>
    /// 周期
    /// </summary>
    CYCLICAL,
    /// <summary>
    /// 持续
    /// </summary>
    PERSISTENT,
}

/// <summary>
/// 移动方式
/// </summary>
public enum ENUM_MOVE
{
    /// <summary>
    /// 瞬移
    /// </summary>
    BLINK,
    /// <summary>
    /// 直线
    /// </summary>
    LINE,
    /// <summary>
    /// 跳跃
    /// </summary>
    JUMP,
    /// <summary>
    /// 潜下
    /// </summary>
    DIVE,
    /// <summary>
    /// 击飞
    /// </summary>
    BLOW,
}
#endregion

#region Effect效果
/// <summary>
/// 神器
/// </summary>
public enum ENUM_ARTIFACT
{
    /// <summary>
    /// 出老千
    /// </summary>
    ONE_MORE,
    /// <summary>
    /// 镜像
    /// </summary>
    MIRROR,
    /// <summary>
    /// 超回复
    /// </summary>
    SUPER_RECOVER,
    /// <summary>
    /// 结算时回复
    /// </summary>
    END_RECOVER,
    /// <summary>
    /// 开始多抽两张
    /// </summary>
    PERFECT_HAND,
    /// <summary>
    /// ?房间不会在碰上敌人
    /// </summary>
    BUDDHA,
}

/// <summary>
/// 效果
/// </summary>
public enum ENUM_EFFECT
{
    /// <summary>
    /// 伤害-血量减少(不恢复)
    /// </summary>
    DAMAGE,
    /// <summary>
    /// 恢复-血量增加(不恢复)
    /// </summary>
    HEAL,
    /// <summary>
    /// 属性改变
    /// </summary>
    ATB_CHANGE,
    /// <summary>
    /// 体型改变
    /// </summary>
    SIZE_CHANGE,
    /// <summary>
    /// 施加增益状态
    /// </summary>
    BOOST,
    /// <summary>
    /// 施加异常状态
    /// </summary>
    ABNOMALY,
    /// <summary>
    /// 加护盾(免疫异常)
    /// </summary>
    SHIELD,
}

/// <summary>
/// 增益状态
/// </summary>
public enum ENUM_BOOST
{

}

/// <summary>
/// 异常状态
/// </summary>
public enum ENUM_ABNOMALY
{
    /// <summary>
    /// 昏厥(原地不动，无法控制)
    /// </summary>
    VERTIGO,
    /// <summary>
    /// 禁锢(原地不动)
    /// </summary>
    IMPRISON,
    /// <summary>
    /// 恐惧(无法控制，远离目标)
    /// </summary>
    THRILL,
    /// <summary>
    /// 寒冷(减速，叠加寒冷转化为冰冻)
    /// </summary>
    CHILL,
    /// <summary>
    /// 冰冻(原地不动，无法控制)
    /// </summary>
    FROZEN,
    /// <summary>
    /// 燃烧(持续伤害)
    /// </summary>
    BURNING,
    /// <summary>
    /// 中毒(持续伤害)
    /// </summary>
    POISONING,
    /// <summary>
    /// 变鸡(无法控制，远离目标)
    /// </summary>
    CHICKEN,
    /// <summary>
    /// 爆炸异常(一段时候后爆炸)
    /// </summary>
    EXPLODE,
    /// <summary>
    /// 麻痹(倒地不动，无法控制)
    /// </summary>
    PARALYSIS,
    /// <summary>
    /// 失明(索敌距离降低为1)
    /// </summary>
    BLIND,
    /// <summary>
    /// 潮湿(水抗降低，火抗提升)
    /// </summary>
    WET,
    /// <summary>
    /// 睡眠(倒地不动，无法控制；受到攻击时解除，并附加额外伤害)
    /// </summary>
    SLEEPING,
    /// <summary>
    /// 魅惑(叛变)
    /// </summary>
    CHARMED,
    /// <summary>
    /// 流血(移动时受到伤害，留下血液地表)
    /// </summary>
    BLEED,
    /// <summary>
    /// 诅咒(恢复转化为伤害)
    /// </summary>
    CURSED,
    /// <summary>
    /// 腐朽(血量上限减半)
    /// </summary>
    DECAYED,
    /// <summary>
    /// 石化(无法移动，无法控制，无法获得其他异常)
    /// </summary>
    // PETRIFIED,
}
#endregion

#region Event事件
/// <summary>
/// Event事件
/// </summary>
public enum ENUM_EVENT
{
    /// <summary>
    /// 游戏通关
    /// </summary>
    GAMECLEAR,
    /// <summary>
    /// 游戏结束
    /// </summary>
    GAMEOVER,
    /// <summary>
    /// 游戏结束
    /// </summary>
    SIDE_CHANGE,
    /// <summary>
    /// 卡牌变动
    /// </summary>
    DECK_CHANGE,
    /// <summary>
    /// 能量变动
    /// </summary>
    ENERGY_CHANGE,
    /// <summary>
    /// 地图事件返回
    /// </summary>
    ME_CALLBACK,
}
#endregion
#endregion

#region Roguelike
/// <summary>
/// MapEvent地图事件
/// </summary>
public enum ENUM_MAP_EVENT
{
    /// <summary>
    /// 无
    /// </summary>
    NONE,
    /// <summary>
    /// 空
    /// </summary>
    EMPTY,
    /// <summary>
    /// 未知
    /// </summary>
    UNKNOWN,
    /// <summary>
    /// 休息
    /// </summary>
    BONFIRE,
    /// <summary>
    /// 商店
    /// </summary>
    SHOP,
    /// <summary>
    /// 宝物
    /// </summary>
    TREASURE,
    /// <summary>
    /// 战斗
    /// </summary>
    BATTLE,
    /// <summary>
    /// 战斗-精英
    /// </summary>
    BATTLE_ELITE,
    /// <summary>
    /// 战斗-BOSS
    /// </summary>
    BATTLE_BOSS,
    /// <summary>
    /// 战斗-宝箱怪
    /// </summary>
    BATTLE_TREASURE,
}
#endregion

#region RPG
/// <summary>
/// Inventory仓库
/// </summary>
public enum ENUM_INVENTORY
{
    /// <summary>
    /// 货币
    /// </summary>
    COIN,
    /// <summary>
    /// 卡牌
    /// </summary>
    CARD,
    /// <summary>
    /// 装备
    /// </summary>
    EQUIP,
    /// <summary>
    /// 药品
    /// </summary>
    POTION,
}
#endregion

#region Starter
/// <summary>
/// 场景
/// </summary>
public enum ENUM_SCENE
{
    /// <summary>
    /// 开始
    /// </summary>
    START,
    /// <summary>
    /// 地图
    /// </summary>
    MAP,
    /// <summary>
    /// 战场
    /// </summary>
    FIELD,
}
#endregion