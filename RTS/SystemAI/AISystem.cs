using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : MonoBehaviour
{
    public int ID;
    SystemAI SystemAI;
    public int IntervalTime = 5;
    float _intervalTime;

    void Start()
    {
        SystemAI = SystemAI.Get(ID);
        FightSystem.Instance.AddCoreB(SystemAI.Core);
        FightSystem.Instance.DeckListB.Deck.AddRange(SystemAI.Deck);
    }

    void Update()
    {
        _intervalTime += Time.deltaTime;
        if (_intervalTime > IntervalTime)
        {
            _intervalTime = 0;
            DrawAll();
            PlayAll();
        }
    }

    void DrawAll()
    {
        for (int i = 0; i < FightSystem.Instance.EnergyB; i++)
        {
            FightSystem.Instance.DrawB();
        }
    }

    void PlayAll()
    {
        for (int i = 0; i < FightSystem.Instance.DeckListB.Hand.Count; i++)
        {
            int cardID = FightSystem.Instance.DeckListB.Hand[i];
            FightSystem.Instance.HandRemoveB(cardID);
            var config = CardConfig.Get(cardID);
            FightSystem.Instance.CreateUnitB(config.Value);
            FightSystem.Instance.GraveAddB(cardID);
        }
    }
}
