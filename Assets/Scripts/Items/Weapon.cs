using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [Range(3.0f, 5000.0f)] public int Damage;
    [Range(1.0f, 5000.0f)] public float Range;
    public TypeWeapon TypeWeapon;
    public Sprite IconWeapon;
}
