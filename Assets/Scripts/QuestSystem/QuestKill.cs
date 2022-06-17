using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Quests/Kill", order = 53)]
public class QuestKill : Quest
{
    public Objectives[] Goals;
    

    public override void Initialize()
    {
        RequiredAmount = new int[Goals.Length];

        for (int i = 0; i < Goals.Length; i++)
        {
            RequiredAmount[i] = Goals[i].RequiredAmount;

        }

        GameManager.Instance.OnEnemyDeath += EnemyDeath;
        base.Initialize();
    }


    private void EnemyDeath(EnemyData enemyData)
    {
        for (int i = 0; i < Goals.Length; i++)
        {
            if(enemyData == Goals[i].profileEnemy)
            {
                CurrentAmount[i]++;
            }
        }

        DataValidation();
    }
}

[System.Serializable]
public class Objectives
{
    public EnemyData profileEnemy;
    public int RequiredAmount;
}
