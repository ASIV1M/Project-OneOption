using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguageController : MonoBehaviour
{
   [SerializeField]private LanguageType _languageType;

    public static LanguageController Instance { get; private set; }
    public LanguageType LanguageType { get => _languageType; set => _languageType = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
