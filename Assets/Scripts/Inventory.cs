using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; // For file handling
using System.Text; // For encoding
using Newtonsoft.Json;
using UnityEditor; // For JSON serialization

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    private string saveFilePath;


    void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventoryData.json");

    }

    void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;

        // // Press 'S' to manually save the inventory
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     SaveInventory();
        // }

        // // Press 'L' to manually load the inventory
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     LoadInventory();
        // }
    }
    // Function to check inventory slot assignment
    [ContextMenu("Check Inventory Slot Assignment")]
    public void CheckInventorySlotAssignment()
    {
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.Log("Inventory slots are not assigned or empty!");
            return;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                Debug.LogError("Slot " + i + " is null!");
            }
            else
            {
                Debug.Log("Slot " + i + " is assigned properly.");
            }
        }
    }
    public void SaveInventory()
    {
        InventoryData data = new InventoryData();

        // Ensure inventorySlots is not null and has elements
        if (inventorySlots == null || inventorySlots.Length == 0)
        {
            Debug.LogWarning("Inventory slots are not assigned or empty.");
            return;
        }

        // Save inventory slots
        foreach (var slot in inventorySlots)
        {
            // Skip if slot is null
            if (slot == null)
            {
                Debug.LogWarning("Null slot found in inventorySlots array, skipping.");
                continue;
            }

            // Skip if no item is in the slot
            if (slot.myItem == null || slot.myItem.myItem == null)
            {
                Debug.Log("Empty slot found, skipping.");
                continue;
            }

            // Save the item data
            ItemData itemData = new ItemData(slot.myItem.myItem.name, slot.myItem.myItem.itemTag);
            data.items.Add(itemData);
        }

        // Convert data to JSON and save it to a file
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Inventory saved: " + jsonData);
    }



    public void LoadInventory()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        // Read the JSON data from the file
        string jsonData = File.ReadAllText(saveFilePath, Encoding.UTF8);
        InventoryData data = JsonConvert.DeserializeObject<InventoryData>(jsonData);

        // Clear existing inventory before loading
        ClearInventory();

        // Load the inventory from the saved data
        foreach (var itemData in data.items)
        {
            Item itemToLoad = System.Array.Find(items, i => i.name == itemData.itemName);
            if (itemToLoad != null)
            {
                SpawnInventoryItem(itemToLoad);
            }
        }

        Debug.Log("Inventory loaded: " + jsonData);
    }

    void ClearInventory()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }
    }


    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        { EquipEquipment(item.activeSlot.myTag, null); }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Chestplate:
                if (item == null)
                {
                    // Destroy item.equipmentPrefab on the Player Object;
                    Debug.Log("Unequipped Chest on " + tag);
                }
                else
                {
                    // Instantiate item.equipmentPrefab on the Player Object;
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                }
                break;

        }
    }

public void SpawnInventoryItem(Item item = null)
{
    Item _item = item;
    if (_item == null)
    {
        _item = PickRandomItem();
    }

    // Get the asset path of the item
    string assetPath = AssetDatabase.GetAssetPath(_item);

    // Check if the item is a .asset file
    if (!assetPath.EndsWith(".asset"))
    {
        Debug.Log("Attempted to spawn a non-asset Item: " + _item.name);
        return; // Prevent spawning a non-asset Item
    }

    for (int i = 0; i < inventorySlots.Length; i++)
    {
        // Check if the slot is empty
        if (inventorySlots[i].myItem == null)
        {
            InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);
            newItem.Initialize(_item, inventorySlots[i]);
            Debug.Log($"Spawned InventoryItem: {_item.name} from path: {assetPath}");
            break;
        }
    }
}




    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        Debug.Log("Picked item: " + items[random]?.name ?? "Null item");

        return items[random];
    }
}
