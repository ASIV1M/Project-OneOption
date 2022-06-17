using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailedQuestPanel : DetailedQuestLog
{
    [SerializeField] private Image[] _questIconReward;
    [SerializeField] private Button _buttonCancelQuest;
    [SerializeField] private QuestController _questController;

    private void OnEnable()
    {
        _buttonCancelQuest?.onClick.AddListener(OnClickButtonCancelQuest);
    }


    private void OnDisable()
    {
        _buttonCancelQuest?.onClick.RemoveListener(OnClickButtonCancelQuest);
    }


    public void OpenQuest()
    {
        this.gameObject.SetActive(true);

        for (int i = 0; i < Quest.Reward.ItemRewards.Length; i++)
        {
            _questIconReward[i].sprite = Quest.Reward.ItemRewards[i].IconItem;
            _questIconReward[i].gameObject.SetActive(true);
        }
    }


    public void OnClickButtonCancelQuest()
    {
       // QuestManager.Instance.RemoveQuest(Quest);
       // _questController.RemoveQuestAtList(quest, Te);
    }
}
