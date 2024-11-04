using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int HOTBAR_SIZE = 5;
    public const int INVENTORY_SIZE = 15;

    public List<InventorySlot> hotbar;
    public List<InventorySlot> inventory;
    public InventorySlot ArmorSlot;
    public int gold;


    private void Awake()
    {
        hotbar = InitializeHotbar(HOTBAR_SIZE);
        inventory = InitializeInventory(INVENTORY_SIZE);
        ArmorSlot = InitializeArmorSlot();
    }

    private List<InventorySlot> InitializeHotbar(int size)
    {
        List<InventorySlot> hotbar = new List<InventorySlot>();
        for (int i = 0; i < size; i++)
        {
            hotbar.Add(new InventorySlot());
        }
        return hotbar;
    }

    private List<InventorySlot> InitializeInventory(int size)
    {
        List<InventorySlot> inventory = new List<InventorySlot>();
        for (int i = 0; i < size; i++)
        {
            inventory.Add(new InventorySlot());
        }
        return inventory;
    }

    private InventorySlot InitializeArmorSlot()
    {
        return new InventorySlot();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}