using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    [SerializeField] private int _moneyCount;

    [SerializeField] private GameObject _objShopUI;
    [SerializeField] protected ShopUI ShopUI;


    private void OnDisable()
    {
        
    }


    private void OnEnable()
    {
        
    }


    public void OpenShop()
    {
        _objShopUI.SetActive(true);
        Initialize();
    }


    public abstract void Initialize();


    private void SellItem(Item item)
    {

    }


    private void BuyItemFromPlayer(Item item)
    {

    }
}


