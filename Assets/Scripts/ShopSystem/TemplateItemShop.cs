using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TemplateItemShop : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameItem;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _iconItem;
    [SerializeField] private TMP_Text _price;


    public void Render(Item item)
    {
        _nameItem.text = item.name;
        _description.text = item.DescriptionItem;
        _iconItem.sprite = item.IconItem;
        _price.text = item.PriceItem.ToString();
    }
}
