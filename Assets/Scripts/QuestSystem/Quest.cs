using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
    public string NameQuest;
    [TextArea(5,10)]
    public string DiscriptionQuest;

    public int[] CurrentAmount { get; set; }
    public int[] RequiredAmount { get; set; }
    
    public Rewards Reward;

  
    [System.Serializable]
    public class Rewards 
    {
        public Item[] ItemRewards;
        public int ExperianceReward;
        public int coinReward;
    }


    public UnityAction<Quest> QuestComplited;
    public bool IsComplite { get; set; }

    

    public virtual void Initialize()
    {
        CurrentAmount = new int[RequiredAmount.Length];
    }


    public void DataValidation()
    {
        for (int i = 0; i < RequiredAmount.Length; i++)
        {
            if (CurrentAmount[i] < RequiredAmount[i])
            {
                IsComplite = false;
                return;
            }
        }
        QuestManager.Instance.QuestComplite = this;
        
        IsComplite = true;
        
        if(IsComplite)
        {
            QuestComplited?.Invoke(this);
        }
        Debug.Log("Quest complite");
    }
}
