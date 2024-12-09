using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public GameObject inventory;
    public GameObject[] hotbar;
    public int selectedSlot = 0;

    public void SetSelectedSlot(int slot)
    {
        SetSlotActive(selectedSlot, false);
        selectedSlot = slot;
        SetSlotActive(selectedSlot, true);
    }

    public void SetSlotActive(int slot, bool active)
    {
        hotbar[slot].GetComponent<HotbarSlot>().SetActiveSlot(active);
    }

    public bool AddItemToHotbar(Item item, int slot)
    {
        HotbarSlot hotbarSlot = hotbar[slot].GetComponent<HotbarSlot>();
        if (hotbarSlot.isEmpty)
        {
            return hotbarSlot.AddItemToHotbar(item);
        }
        Debug.Log("Hotbar slot is not empty.");
        return false;
    }

    public void RemoveItemFromHotbar(int slot)
    {
        HotbarSlot hotbarSlot = hotbar[slot].GetComponent<HotbarSlot>();
        if (!hotbarSlot.isEmpty)
        {
            Item itemToRemove = hotbarSlot.item;
            hotbarSlot.RemoveItem();
            Debug.Log("Item removed from hotbar slot " + slot);

            // Add the item back to the inventory
            Inventory inventoryComponent = inventory.GetComponent<Inventory>();
            if (inventoryComponent != null)
            {
                inventoryComponent.AddItem(itemToRemove);
            }
        }
        else
        {
            Debug.Log("Hotbar slot is already empty.");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (!inventory.GetComponent<Inventory>().InventoryOpen)
        {
            switch (Input.inputString)
            {
                case "1":
                    Debug.Log("Selected Slot 1");
                    SetSelectedSlot(0);
                    break;
                case "2":
                    Debug.Log("Selected Slot 2");
                    SetSelectedSlot(1);
                    break;
                case "3":
                    Debug.Log("Selected Slot 3");
                    SetSelectedSlot(2);
                    break;
                case "4":
                    Debug.Log("Selected Slot 4");
                    SetSelectedSlot(3);
                    break;
                case "5":
                    Debug.Log("Selected Slot 5");
                    SetSelectedSlot(4);
                    break;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                SetSelectedSlot((selectedSlot + 1) % hotbar.Length);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                SetSelectedSlot((selectedSlot - 1 + hotbar.Length) % hotbar.Length);
            }
        }
    }
}