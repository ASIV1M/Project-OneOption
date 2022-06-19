using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : Shop
{
    [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

    public override void Initialize()
    {
        ShopUI.OpenShopPanel();
    }
}
