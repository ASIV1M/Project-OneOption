using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string NameItem;
    [TextArea(5, 10)] public string DescriptionItem;
    public int PriceItem;
    public Sprite IconItem;
    public string Name => NameItem;
    public string Description => DescriptionItem;
    public int Price => Price;

    public GameObject _prefabItem;
}
