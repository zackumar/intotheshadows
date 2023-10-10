using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    Item item;

    public void AddItem(Item item)
    {
        this.item = item;
        icon.sprite = item.ItemSprite;
        icon.enabled = true;
    }

    public void ClearItem()
    {
        this.item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

}
