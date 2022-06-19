using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject _objStorePanel;
    [SerializeField] private GameObject _objStoreCharacter;
    [SerializeField] private GameObject _objStorePlayer;

    [SerializeField] private Button _buttonSell;
    [SerializeField] private Button _buttonBuy;

    [SerializeField] private StorePlayer _storePlayer;
    [SerializeField] private StoreCharacter _storeCharacter;

    private Item _item;

    private void OnEnable()
    {
        _buttonSell.onClick?.AddListener(OnClickSell);
        _buttonBuy.onClick?.AddListener(OnClickBuy);
    }


    private void OnDisable()
    {
        _buttonSell.onClick?.RemoveListener(OnClickSell);
        _buttonBuy.onClick?.RemoveListener(OnClickBuy);
    }


    public void OpenShopPanel()
    {
        _objStorePanel.SetActive(true);
    }


    private void OnClickSell()
    {
        _objStorePlayer.SetActive(true);
        this.gameObject.SetActive(false);
    }


    private void OnClickBuy()
    {
        _objStoreCharacter.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
