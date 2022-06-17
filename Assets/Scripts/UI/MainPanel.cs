using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    /*[SerializeField] private DatasPanel[] _datasPanel;

    private DatasPanel _currentPanel;
    private int _currentIndexPanel;
    private int _pastIndex;

    private void Awake()
    {
        for (int i = 0; i < _datasPanel.Length; i++)
        {
            if (_datasPanel[i].EnabledPanel)
            {
                _currentPanel = _datasPanel[i];
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < _datasPanel.Length; i++)
        {
            if (_datasPanel[i].UiObjects.activeSelf == true)
            {
                if (_datasPanel[i].Index != _currentPanel.Index)
                {
                    _currentPanel.UiObjects.SetActive(false);
                    _pastIndex = _currentPanel.Index;
                    _currentPanel = _datasPanel[i];
                }
                if(_currentPanel.UiObjects.activeSelf == false)
                {
                    _currentPanel.Index = _pastIndex;

                    if(_datasPanel[i] == _currentPanel)
                        _currentPanel.UiObjects.SetActive(true);
                }
            }
        }
    }*/
}

[System.Serializable]
public class DatasPanel
{
    public GameObject UiObjects;
    public int Index;
    public bool EnabledPanel; 
}
