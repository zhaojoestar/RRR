using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Priest : AIBase_Monster
{
    MagicConfig magicC;
    int skillID;
    float skillCD;
    float _skillCD;
    new void Start()
    {
        base.Start();
        var card = GetComponent<Property>().CardID;
        var _unitID = CardConfig.Get(card).Value;
        skillID = UnitConfig.Get(_unitID).Skill;
        skillCD = UnitConfig.Get(_unitID).SkillCD;
        magicC = MagicConfig.Get(skillID);
    }

    new void Update()
    {
        base.Update();

        if (FightSystem.Instance.isFightOver) return;
        if (_isDead) return;
        _skillCD += Time.deltaTime;
        //显示CD
        if (_skillCD >= skillCD)
        {
            _skillCD = 0;
            base.OnSkill();
            MagicMgr.Init(magicC, transform, ppt);
        }
    }
}
