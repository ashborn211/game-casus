using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int NOT_SELECTED = -1;

    public GameObject hotbar;
    public GameObject[] inventory;
    public InventorySlot ArmorSlot;
    public ulong gold = 0;

    [Header("Gold Text")]
    public TMPro.TextMeshProUGUI goldText;

    [Header("Item List")]
    [SerializeField] private Item[] items;

    [Header("Armor List")]
    [SerializeField] private Item[] armorItems;

    public int selectedSlot = NOT_SELECTED;

    public GameObject inventoryPanel;
    public bool InventoryOpen = false;

    private InventorySlot InitializeArmorSlot()
    {
        return new InventorySlot();
    }

    public void OnClick(int slot)
    {
        slot--;
        Debug.Log("Slot " + slot + " clicked");

        InventorySlot clickedSlot = GetSlot(slot);

        Debug.Log("Slot Selected" + selectedSlot);

        if (slot == selectedSlot)
        {
            DeselectItem();
            return;
        }

        if (selectedSlot != NOT_SELECTED)
        {
            if (!clickedSlot.AddItem(GetItem(selectedSlot)))
            {
                return;
            }
            GetSlot(selectedSlot).RemoveItem();
            GetSlot(selectedSlot).RemoveHighlight();
            selectedSlot = NOT_SELECTED;
        }
        else
        {
            if (clickedSlot.item == null)
            {
                Debug.Log("Slot is empty.");
                return;
            }
            SelectSlot(slot);
            GetSlot(slot).AddHighlight();
        }
    }

    public void DeselectItem()
    {
        if (selectedSlot == NOT_SELECTED)
        {
            return;
        }
        GetSlot(selectedSlot).RemoveHighlight();
        selectedSlot = NOT_SELECTED;
    }

    public InventorySlot GetSlot(int slot)
    {
        return inventory[slot].GetComponent<InventorySlot>();
    }

    public Item GetItem(int slot)
    {
        return GetSlot(slot).item;
    }

    public void SelectSlot(int Slot)
    {
        selectedSlot = Slot;
    }

    public void AddGold(int amount)
    {
        this.gold = this.gold + (ulong)amount;
    }

    public void RemoveGold(int amount)
    {
        this.gold = this.gold - (ulong)amount;
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            InventorySlot slot = inventory[i].GetComponent<InventorySlot>();
            if (slot.isEmpty)
            {
                slot.AddItem(item);
                Debug.Log("Item added to inventory.");
                return;
            }
        }

        Debug.Log("Inventory is full.");
    }

    public void AddItemToHotbar(Item item, int hotbarSlot)
    {
        if (hotbar.GetComponent<Hotbar>().AddItemToHotbar(item, hotbarSlot))
        {
            // Find the slot containing the item and remove it
            for (int i = 0; i < inventory.Length; i++)
            {
                InventorySlot slot = inventory[i].GetComponent<InventorySlot>();
                if (slot.item == item)
                {
                    slot.RemoveItem();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Failed to add item to hotbar.");
        }
    }

    public void GiveRandomItem()
    {
        int randomIndex = Random.Range(0, items.Length);
        AddItem(items[randomIndex]);
    }

    public void ToggleInventory()
    {
        InventoryOpen = !InventoryOpen;
        inventoryPanel.SetActive(InventoryOpen);
        DeselectItem();
    }

    void Start()
    {
        ToggleInventory();
        goldText.text = gold.ToString("N0");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        goldText.text = "" + gold;

        for (int i = 0; i < inventory.Length; i++)
        {
            InventorySlot slot = inventory[i].GetComponent<InventorySlot>();
            if (slot.IsHovered())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    AddItemToHotbar(slot.item, 0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    AddItemToHotbar(slot.item, 1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    AddItemToHotbar(slot.item, 2);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    AddItemToHotbar(slot.item, 3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    AddItemToHotbar(slot.item, 4);
                }
            }
        }
    }
}