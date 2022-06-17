using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/New color Event")]
[System.Serializable]
public class EventRandomColors : DialogueEventSO
{
    public override void RunEvent()
    {
        GameEvents.Instance.CallRandomColorModel();
    }
}
