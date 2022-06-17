using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New quest Event")]
public class EventQuestGiver : DialogueEventSO
{
    public override void RunEvent()
    {
        GameEvents.Instance.CallGiveQuest();
    }
}
