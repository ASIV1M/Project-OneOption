using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DetailedQuestLog : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameQuest;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private List<Button> _buttosQuests = new List<Button>();

    [SerializeField] private GameObject _objQuestDetail;
    [SerializeField] private DetailedQuestPanel _detailedQuestPanel;

    protected Quest Quest;

    private void OnEnable()
    {
        if(_buttosQuests != null)
        {
            foreach (Button bt in _buttosQuests)
            {
                bt.onClick.AddListener(_detailedQuestPanel.OpenQuest);
            }
        }
    }


    private void OnDisable()
    {
        foreach (Button bt in _buttosQuests)
        {
            bt.onClick.RemoveListener(_detailedQuestPanel.OpenQuest);
        }
    }


    public void OpenDetailedQuestLog(Quest quest, Button questButton)
    {
        _nameQuest.text = quest.NameQuest;
        _description.text = quest.DiscriptionQuest;

        Quest = quest;
    }
}
