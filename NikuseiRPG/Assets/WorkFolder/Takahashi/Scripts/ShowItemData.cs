using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemData : MonoBehaviour
{
    private ItemData itemData;
    public Image image;
    public Text itemName;
    public Text price;

    public void SetItemData(int id)
    {
        itemData = ItemMasterData.GetValue(id);
        image.sprite = itemData.sprite;
        itemName.text = itemData.name;
        price.text = itemData.price.ToString();
    }

    public int GetPrice
    {
        get
        {
            return itemData.price;
        }
    }
}
