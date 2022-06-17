using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestController: MainPanel
{
    [SerializeField] protected QuestView TemplateSimple;
    [SerializeField] protected QuestView TemplateDetailed;
    
    [SerializeField] protected GameObject SimpleContainer;
    [SerializeField] protected GameObject DetailedContainer;

    [SerializeField] private GameObject _detailedQuestPanel;

    [SerializeField] private StarterAssets.StarterAssetsInputs _inputs;
    private SimpleQuestView _simplePanel;
    private TemplateDetailedQuest _detailedPanel;

    private List<TemplateDetailedQuest> _detaledQuests = new List<TemplateDetailedQuest>();
    private List<SimpleQuestView> _simpleQuestViews = new List<SimpleQuestView>();

    private Quest _quest;
    private int _countPresed = 0;

    
    private void Update()
    {
        if (_inputs.openQuestLog && _countPresed == 0)
        {
            _countPresed = 1;
            _detailedQuestPanel.SetActive(true);
            _inputs.openQuestLog = false;
        }
        else if(_countPresed == 1 && _inputs.openQuestLog)
        {
            _countPresed = 0;
            _detailedQuestPanel.SetActive(false);
            _inputs.openQuestLog = false;
        }

        if (_simplePanel == null)
            return;
        
        if (_detailedPanel == null)
            return;
    }


    public void AddItem(Quest quest)
    {

        foreach (Quest item in QuestManager.Instance.QuestList)
        {
            AddItemSimpleLog(item);
            AddItemDetailedLog(item);
        }
    }


    public void AddItemDetailedLog(Quest quest)
    {
        var view = Instantiate(TemplateDetailed, DetailedContainer.transform);
        view.Render(quest);

        _detailedPanel = view.GetComponent<TemplateDetailedQuest>();

        _detaledQuests.Add(_detailedPanel);
    }


    public void AddItemSimpleLog(Quest quest)
    {
        var view = Instantiate(TemplateSimple, SimpleContainer.transform);
        view.Render(quest);

        _simplePanel = view.GetComponent<SimpleQuestView>();
        _simpleQuestViews.Add(_simplePanel);
    }


    public void RemoveQuestAtList(Quest quest)
    {
        foreach (SimpleQuestView simple in _simpleQuestViews)
        {
            if(quest == simple.Quest)
                Destroy(simple.gameObject);
        }
    }
}
