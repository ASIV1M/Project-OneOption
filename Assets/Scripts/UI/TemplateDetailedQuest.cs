using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemplateDetailedQuest : QuestView
{
    [SerializeField] private Button _buttonQuest;

    private Quest _quest;

    private void OnEnable()
    {
        _buttonQuest.onClick.AddListener(OnClickQuest);
    }

    private void OnDisable()
    {
        _buttonQuest?.onClick.RemoveListener(OnClickQuest);
    }


    public override void Render(Quest quest)
    {
        NameQuest.text = quest.NameQuest;
        _quest = quest;
    }


    public void OnClickQuest()
    {
        QuestManager.Instance.OpenQuestDetail(_quest, _buttonQuest);
    }
}
