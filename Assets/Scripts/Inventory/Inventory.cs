using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Hotbar hotbar;
    public GameObject[] inventory;
    public InventorySlot ArmorSlot;
    public int gold;

    [Header("Item List")]
    [SerializeField] private Item[] items;

    private Item selectedItem;

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

        InventorySlot clickedSlot = inventory[slot].GetComponent<InventorySlot>();

        if (selectedItem != null)
        {
            // Assign the selected item to the clicked slot
            if (clickedSlot.isEmpty)
            {
                clickedSlot.AddItem(selectedItem);
                clickedSlot.isEmpty = false;
                selectedItem = null; 
                Debug.Log("Item assigned to slot.");
            }
            else
            {
                Debug.Log("Slot is already occupied.");
            }
        }
        else
        {
            // Select the item from the clicked slot if it's not empty
            if (!clickedSlot.isEmpty)
            {
                selectedItem = clickedSlot.item;
                clickedSlot.RemoveItem();
                clickedSlot.isEmpty = true;
                Debug.Log("Item selected from slot.");
            }
            else
            {
                Debug.Log("Slot is empty.");
            }
        }
    }

    public void ToggleInventory()
    {
        InventoryOpen = !InventoryOpen;
        inventoryPanel.SetActive(InventoryOpen);
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(InventoryOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }
}
