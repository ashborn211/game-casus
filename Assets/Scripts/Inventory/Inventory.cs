using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int NOT_SELECTED = -1;

    public GameObject hotbar;
    public GameObject[] inventory;
    public InventorySlot ArmorSlot;
    public int gold = 0;

    [Header("Gold Text")]
    public TMPro.TextMeshProUGUI goldText;


    [Header("Item List")]
    [SerializeField] private Item[] items;

    [Header("Armor List")]
    [SerializeField] private Item[] armorItems;

    private int selectedSlot = NOT_SELECTED;

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
        gold += amount;
    }

    public void RemoveGold(int amount)
    {
        gold -= amount;
    }

    public void AddItem(Item item)
    {
        // Add the item to the first empty slot
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

    // Start is called before the first frame update
    void Start()
    {
        ToggleInventory();
        goldText.text = "" + gold;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        goldText.text = "" + gold;
    }
}
