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
    [SerializeField] Item[] items;

    public GameObject inventoryPanel;
    public bool InventoryOpen = false;

    private InventorySlot InitializeArmorSlot()
    {
        return new InventorySlot();
    }

    public void onclick(int slot)
    {
        slot--;
        Debug.Log("Slot " + slot + " clicked");
        // get the gameObject script and run the function
        inventory[slot].GetComponent<InventorySlot>().AddItem();
    }

    public void ToggleInventory()
    { //false         = !false = true
        InventoryOpen = !InventoryOpen;
        inventoryPanel.SetActive(InventoryOpen);
    }

    public void GiveRandomItem()
    {
        // Get a random item
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