using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; // For file handling
using System.Text; // For encoding
using Newtonsoft.Json;
using UnityEditor;
using static Item; // For JSON serialization



[System.Serializable]

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
    }



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

        foreach (var slot in inventorySlots)
        {
            if (slot.myItem == null || slot.myItem.myItem == null)
            {
                Debug.Log("Empty slot found, skipping.");
                continue;
            }

            ItemData itemData = new ItemData(slot.myItem.myItem.name, slot.myItem.myItem.itemType);
            data.items.Add(itemData);
        }

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

        string jsonData = File.ReadAllText(saveFilePath, Encoding.UTF8);
        InventoryData data = JsonConvert.DeserializeObject<InventoryData>(jsonData);

        ClearInventory();

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
            if (item.activeSlot.armorType != ArmorType.None &&
                item.activeSlot.armorType != (ArmorType)carriedItem.myItem.armorType)
                return;

            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.armorType != ArmorType.None)
        {
            EquipEquipment(item.activeSlot.armorType, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(ArmorType armorType, InventoryItem item = null)
    {
        switch (armorType)
        {
            case ArmorType.Helmet:
                Debug.Log(item == null ? "Unequipped Helmet." : "Equipped Helmet: " + item.myItem.name);
                break;
            case ArmorType.Chestplate:
                Debug.Log(item == null ? "Unequipped Chestplate." : "Equipped Chestplate: " + item.myItem.name);
                break;
            case ArmorType.Leggings:
                Debug.Log(item == null ? "Unequipped Leggings." : "Equipped Leggings: " + item.myItem.name);
                break;
            case ArmorType.Boots:
                Debug.Log(item == null ? "Unequipped Boots." : "Equipped Boots: " + item.myItem.name);
                break;
            default:
                Debug.Log("Invalid armor type.");
                break;
        }
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item ?? PickRandomItem();
        string assetPath = AssetDatabase.GetAssetPath(_item);

        if (!assetPath.EndsWith(".asset"))
        {
            Debug.Log("Attempted to spawn a non-asset Item: " + _item.name);
            return;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
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
