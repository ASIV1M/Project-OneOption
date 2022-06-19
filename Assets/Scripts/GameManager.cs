using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private Inventory _inventory;

    public static GameManager Instance { get; private set; }
    public RewardManager RewardManager { get; private set; }
    public QuestManager QuestManager { get; set; }

    public UnityAction<Item, int> SellItem;
    public UnityAction<Item, int> BuyItem;

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


    private void Buy(Item item)
    {
        
    }


    public void Sell()
    {
       

    }


    public delegate void OnEnemyDeathCallBack(EnemyData enemyProfile);
    public OnEnemyDeathCallBack OnEnemyDeath;

    
}
