using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asset/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public CharacterProfile Character;

    }

    public Info[] dialogueInfo;
}
