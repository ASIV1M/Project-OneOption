using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SimpleQuestView: QuestView
{
    public UnityAction<Quest, SimpleQuestView> CompliteQuest;


    public Quest GetQuest => Quest;
    private void Update()
    {
        RefrashQuest();
    }

    private void RefrashQuest()
    {
        for (int i = 0; i < Quest.RequiredAmount.Length; i++)
        {
            CurrentAmount.text = Quest.CurrentAmount[i].ToString();
        }
    }


    public override void Render(Quest quest)
    {
        Quest = quest;

        NameQuest.text = quest.name;
        Description.text = quest.DiscriptionQuest;

        for (int i = 0; i < quest.RequiredAmount.Length; i++)
        {
            CurrentAmount.text = quest.CurrentAmount[i].ToString();
            RequiredAmount.text = quest.RequiredAmount[i].ToString();
        }
    }
}
