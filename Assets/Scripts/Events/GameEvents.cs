using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private event Action _randomColorModel;
    private event Action _giveQuest;

    public static GameEvents Instance { get; private set; }
    public Action RandomColorModel { get => _randomColorModel; set => _randomColorModel = value; }
    public Action GiveQuest { get => _giveQuest; set => _giveQuest = value; }
    

    private void Awake()
    {
        Instance = this;
    }


    public void CallRandomColorModel()
    {
        _randomColorModel?.Invoke();
    }

    public void CallGiveQuest()
    {
        _giveQuest?.Invoke();
    }
}
