using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Knight : AIBase_Monster
{
    new void Start()
    {
        base.Start();
    }

    public float skillCD;
    float _skillCD;
    new void Update()
    {
        base.Update();

        if (FightSystem.Instance.isFightOver) return;
        if (_isDead) return;
        _skillCD += Time.deltaTime;
        //显示CD
        if (_skillCD <= skillCD)
        {
            base.OnSkill();
        }
    }
}
