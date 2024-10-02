using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotTag { None, Head, Chestplate, Legs, Feet }

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }

    public SlotTag myTag;
    public Item item; // Reference to the item stored in this slot
    public Image itemIcon; // The UI image component to display the item's icon

    public void UpdateSlotUI()
    {
        if (item != null)
        {
            itemIcon.sprite = item.icon; // Assuming `item.icon` holds the sprite of the item
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
        }
    }

    public void ClearSlot()
    {
        item = null;
        UpdateSlotUI();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        // Reset old slot
        item.activeSlot.myItem = null;

        // Set current slot
        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
        { Inventory.Singleton.EquipEquipment(myTag, myItem); }
    }
}
