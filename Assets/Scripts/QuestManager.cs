using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject _questUI;
    [SerializeField] private TMP_Text _questName;
    [SerializeField] private TMP_Text _questDiscription;

    [SerializeField] private Button _buttonAcceptQuest;
    [SerializeField] private Button _buttonCancelQuest;

    [SerializeField] private QuestController questController;

    [SerializeField] private DetailedQuestLog _detailedQuestLog;
    [SerializeField] private GameObject _objQuestDetailedLog;
    [SerializeField] private DetailedQuestPanel _questDetailedPanel;

    private List<Quest> _questList = new List<Quest>();
    private SimpleQuestView _simplePanel;


    public static QuestManager Instance { get; private set; }

    private Quest _quest;

    public bool ComplitedQuestReady {get; set; }
    public UnityAction AcceptQuest;
    public bool InQuestUI { get; set; }

    public Quest QuestComplite { get; set; }
    public List<Quest> QuestList => _questList;

   
    private void OnEnable()
    {
        _buttonAcceptQuest.onClick?.AddListener(AcceptClick);
        _buttonCancelQuest.onClick?.AddListener(ClosePanel);

    }


    private void OnDisable()
    {
        _buttonAcceptQuest.onClick?.RemoveListener(AcceptClick);
        _buttonCancelQuest.onClick?.RemoveListener(ClosePanel);
    }


    private void Awake()
    {
        Instance = this;
    }


    public void SetQuestUI(Quest newQuest)
    {
        _questUI.SetActive(true);
        _questName.text = newQuest.NameQuest;
        _questDiscription.text = newQuest.DiscriptionQuest;
        _quest = newQuest;

        _questList.Add(newQuest);
        newQuest.QuestComplited += RemoveQuest;
    }


    private void AcceptClick()
    {
        if (_quest != null)
        {
            questController.AddItem(_quest);
            ClosePanel();
        }
    }


    private void ClosePanel()
    {
        _questUI.SetActive(false);
    }

    
    public void OpenQuestDetail(Quest quest, Button ButtonQuest)
    {
        _objQuestDetailedLog.SetActive(true);
        _detailedQuestLog.OpenDetailedQuestLog(quest, ButtonQuest);
    }


    public void RemoveQuest(Quest quest)
    {
        foreach (Quest item in _questList)
        {
            if(item == quest)
            {
                quest.QuestComplited -= RemoveQuest;
                questController.RemoveQuestAtList(item);
                GameManager.Instance.RewardManager.SetRewardUI(item);
                //_questList.Remove(item);
            }
        }
    }


    
}
