using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton { get; private set; }
    public InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[15];
    [SerializeField] InventorySlot[] hotbarSlots = new InventorySlot[5];
    [SerializeField] InventorySlot equipmentSlot;
    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;
    [Header("Item List")]
    [SerializeField] Item[] items;
    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    private PlayFabInventorySync playFabInventorySync;

    public InventorySlot[] GetInventorySlots() => inventorySlots;
    public InventoryItem GetItemPrefab() => itemPrefab;
    public Item[] GetItems() => items;

    void Awake()
    {
        // Singleton setup
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Assign Give Item Button functionality
        if (giveItemBtn != null)
        {
            giveItemBtn.onClick.AddListener(SpawnInventoryItem);
        }
        else
        {
            Debug.LogError("giveItemBtn is not assigned.");
        }

        // Setup PlayFab inventory sync
        playFabInventorySync = FindObjectOfType<PlayFabInventorySync>();
        if (playFabInventorySync == null)
        {
            Debug.LogError("PlayFabInventorySync not found in the scene.");
        }
    }

    void Update()
    {
        // Update position of carried item to follow the mouse
        if (carriedItem == null) return;
        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        // If there's already a carried item, attempt to place it into the active slot
        if (carriedItem != null)
        {
            if (item != null && item.activeSlot != null && item.activeSlot.myItem != null)
            {
                if (item.activeSlot.myItem.itemTag != carriedItem.myItem.itemTag) return; // Tags don't match
            }

            item.activeSlot.SetItem(carriedItem); // Place the carried item
        }

        // Unequip item if moving from an equipment slot
        if (item != null && item.activeSlot != null)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }

        // Set the new carried item
        carriedItem = item;
        if (carriedItem != null)
        {
            carriedItem.canvasGroup.blocksRaycasts = false; // Make item draggable
            carriedItem.transform.SetParent(draggablesTransform); // Attach to draggables transform
        }
    }

    public void EquipEquipment(string tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case "Chest":
                if (item == null)
                {
                    // Unequip the item
                    Debug.Log("Unequipped item on " + tag);
                }
                else
                {
                    // Equip the item
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                }
                break;
                // Add more cases for other equipment slots if necessary
        }
    }

    public void SpawnInventoryItem()
    {
        // Pick a random item to spawn
        Item _item = PickRandomItem();

        if (_item == null)
        {
            Debug.LogError("No item found to spawn.");
            return;
        }

        // Find an empty slot in the inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                // Instantiate the inventory item in the slot
                InventoryItem inventoryItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                if (inventoryItem != null)
                {
                    inventoryItem.Initialize(_item, inventorySlots[i]);

                    // Sync with PlayFab
                    if (playFabInventorySync != null)
                    {
                        playFabInventorySync.AddItemToInventory(_item, 1); // Add 1 quantity
                    }
                }
                else
                {
                    Debug.LogError("Failed to instantiate itemPrefab.");
                }
                break;
            }
        }
    }

    Item PickRandomItem()
    {
        if (items.Length == 0)
        {
            Debug.LogError("Item list is empty.");
            return null;
        }

        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
