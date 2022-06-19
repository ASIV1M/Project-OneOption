using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class QuestView : MonoBehaviour
{
    [SerializeField] protected TMP_Text NameQuest;
    [SerializeField] protected TMP_Text Description;
    [SerializeField] protected TMP_Text CurrentAmount;
    [SerializeField] protected TMP_Text RequiredAmount;
    
    protected Quest Quest;

    public UnityAction<QuestView> OpenPanelView;

    public abstract void Render(Quest quest);
}
