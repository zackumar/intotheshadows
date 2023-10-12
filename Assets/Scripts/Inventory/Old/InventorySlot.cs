using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image icon;

    Item item;
    private Button button;

    public TextMeshProUGUI tooltip;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void AddItem(Item item)
    {
        this.item = item;
        icon.sprite = item.ItemSprite;
        icon.enabled = true;
        button.interactable = true;

    }

    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        button.interactable = false;
    }

    public void Use()
    {
        if (!item) return;
        UIHandler.SetCursor(item.ItemSprite.texture);
        item.Use(FindObjectsByType<PlayerController>(FindObjectsSortMode.None)[0].transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!item) return;
        tooltip.text = item.DisplayName;
        tooltip.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!item) return;
        tooltip.text = "";
        tooltip.enabled = false;

    }
}
