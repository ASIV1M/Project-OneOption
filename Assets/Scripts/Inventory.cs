using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _templateItem;

    private List<Item> _items = new List<Item>();

    public List<Item> Items => _items;

    public static Inventory Instance { get; private set; }



    private void Start()
    {
        Instance = this;
    }


    public void AddItem(Item item)
    {
        _items.Add(item);

        foreach (Item ite in _items)
        {
            AddInInvenotry(ite);
        }
    }


    private void AddInInvenotry(Item item)
    {

    }
}
