using UnityEngine;
using UnityEngine.UI;

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

    private PlayFabInventorySync playFabInventorySync;

    void Awake()
    {
        Singleton = this;
        if (giveItemBtn != null)
        {
            giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
        }
        else
        {
            Debug.LogError("giveItemBtn is not assigned.");
        }

        // Initialize PlayFabInventorySync
        playFabInventorySync = FindObjectOfType<PlayFabInventorySync>();
        if (playFabInventorySync == null)
        {
            Debug.LogError("PlayFabInventorySync not found in the scene.");
        }
    }


    void Update()
    {
        if (carriedItem == null) return;
        carriedItem.transform.position = Input.mousePosition;
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
            case SlotTag.Chest:
                if (item == null)
                {
                    // Destroy item.equipmentPrefab on the Player Object;
                    Debug.Log("Unequipped item on " + tag);
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

        if (_item == null)
        {
            Debug.LogError("No item found to spawn.");
            return;
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                InventoryItem inventoryItem = Instantiate(itemPrefab, inventorySlots[i].transform);
                if (inventoryItem != null)
                {
                    inventoryItem.Initialize(_item, inventorySlots[i]);
                    // Call AddItemToInventory on PlayFabInventorySync
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
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
