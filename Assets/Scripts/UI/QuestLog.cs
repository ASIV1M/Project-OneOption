using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : QuestView
{
    [SerializeField] private Button _buttonCloseLog;
    [SerializeField] private Button _buttonCancelQuest;
    [SerializeField] private GameObject _questLog;


    private void Start()
    {
        _questLog.SetActive(false);
    }


    private void OnEnable()
    {
        _buttonCloseLog.onClick.AddListener(CloseQuestLog);
    }


    private void OnDisable()
    {
        _buttonCloseLog.onClick.RemoveListener(CloseQuestLog);
    }


    public override void Render(Quest quest)
    {
        NameQuest.text = quest.name;
        Description.text = quest.DiscriptionQuest;

        for (int i = 0; i < quest.RequiredAmount.Length; i++)
        {
            CurrentAmount.text = quest.CurrentAmount[i].ToString();
            RequiredAmount.text = quest.RequiredAmount[i].ToString();
        }
    }


    public void CloseQuestLog()
    {
        _questLog.SetActive(false);
    }
}
