using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private GameObject _shopBandle;

    private Player _player;


    private void Update()
    {
        if (_player != null)
        {
            if (_player.IsInteract && _shopBandle.activeSelf)
            {
                _shop.OpenShop();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _player = player;
            _shopBandle.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _player = null;
            _shopBandle.SetActive(false);
        }
    }
}
