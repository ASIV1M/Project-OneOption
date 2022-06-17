using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private QuestManager _questManager;

    public static GameManager Instance { get; private set; }
    public RewardManager RewardManager { get; private set; }
    public QuestManager QuestManager { get; set; }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    private void Awake()
    {
        Instance = this;
        RewardManager = _rewardManager;
        QuestManager = _questManager;
    }


    public delegate void OnEnemyDeathCallBack(EnemyData enemyProfile);
    public OnEnemyDeathCallBack OnEnemyDeath;

    
}
