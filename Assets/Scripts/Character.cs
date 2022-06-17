using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IInteraction
{
    [SerializeField] private string _nameCharacter;

    public void Interaction()
    {
        Debug.Log("I interaction for you");
    }


    public void UnInteraction()
    {
        Debug.Log("I can't interaction for you");   
    }
}
