using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public TMP_Text NameQuest;
    public Image[] QuestRewardIcons;
    public GameObject QuestRewardUI;
    public Button ButtonClaim;

    public bool InQuestReward { get; set; }


    private void OnEnable()
    {
        ButtonClaim?.onClick.AddListener(Claim);
    }


    private void OnDisable()
    {
        ButtonClaim?.onClick.RemoveListener(Claim);
    }


    public void SetRewardUI(Quest quest)
    {
        InQuestReward = true;
        QuestRewardUI.SetActive(true);
        NameQuest.text = quest.NameQuest;

        ButtonClaim.Select();
        

        for (int i = 0; i < quest.Reward.ItemRewards.Length; i++)
        {
            QuestRewardIcons[i].gameObject.SetActive(true);
            QuestRewardIcons[i].sprite = quest.Reward.ItemRewards[i].IconItem;
        }
    }


    public void Claim()
    {
        Quest currentQuest = QuestManager.Instance.QuestComplite;

        for (int i = 0; i < QuestRewardIcons.Length; i++)
        {
            QuestRewardIcons[i].gameObject.SetActive(true);
        }

        QuestRewardUI.SetActive(false);

        for (int i = 0; i < QuestManager.Instance.QuestComplite.Reward.ItemRewards.Length; i++)
        {
            Inventory.Instance.AddItem(currentQuest.Reward.ItemRewards[i]);
        }

        StartCoroutine(QuestRewardBuffer());
    }


    IEnumerator QuestRewardBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        InQuestReward = false;
    }
}
