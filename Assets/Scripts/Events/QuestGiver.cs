using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private List<Quest>_quests = new List<Quest>();
    [SerializeField] private GameObject _questBandle;
    [SerializeField] private TalkZone _talkZone;

    public bool HasActiveQuest;

    private void Start()
    {
        GameEvents.Instance.GiveQuest += GiveQuest;

        foreach(Quest item in _quests)
        {
            item.Initialize();
            HasActiveQuest = true;
        }
    }


    private void OnDestroy()
    {
        GameEvents.Instance.GiveQuest -= GiveQuest;
    }


    private void Update()
    {
        if (HasActiveQuest)
        {
            _questBandle.SetActive(true);

            if (_talkZone.isTalk)
            {
                _questBandle.SetActive(false);
            }
        }

        if (QuestManager.Instance.ComplitedQuestReady)
        {
            _questBandle.SetActive(false);
        }
    }


    public void GiveQuest()
    {
        Debug.Log("Quest giver player");

        foreach (Quest item in _quests)
        {
            QuestManager.Instance.SetQuestUI(item);
        }
    }
}
