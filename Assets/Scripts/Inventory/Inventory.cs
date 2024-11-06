using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Hotbar hotbar;
    [SerializeField] public GameObject[] inventory;
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
    }

    public void ToggleInventory()
    { //false         = !false = true
        InventoryOpen = !InventoryOpen;
        inventoryPanel.SetActive(InventoryOpen);
    }

    public void GiveRandomItem()
    {
        inventory[0].AddItem(items[Random.Range(0, items.Length)]);
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