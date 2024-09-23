using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;

public class PlayFabInventorySync : MonoBehaviour
{
    public InventoryDatabase localInventory;
    public InventoryItem itemPrefab;
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;
    [SerializeField] InventorySlot[] equipmentSlots;

    private bool isLoggedIn = false;

    void Start()
    {
        if (localInventory == null)
        {
            Debug.LogError("LocalInventory is not assigned.");
            return;
        }

        StartCoroutine(CheckLoginStatus());
    }

    private IEnumerator CheckLoginStatus()
    {
        while (!PlayFabLogin.IsLoggedIn)
        {
            yield return new WaitForSeconds(1f);
        }

        isLoggedIn = true;
        LoadInventoryFromPlayFab();
    }

    public void AddItemToInventory(Item item, int qty)
    {
        if (item == null || localInventory == null)
        {
            Debug.LogError("Invalid item or local inventory.");
            return;
        }

        if (string.IsNullOrEmpty(item.name) || string.IsNullOrEmpty(item.displayName))
        {
            Debug.LogError("Item data is invalid.");
            return;
        }

        ItemSaveData newItem = new ItemSaveData(item.name, item.displayName, qty, item.itemTag);
        localInventory.AddItem(newItem);
        SaveInventoryToPlayFab();
    }

    public void SaveInventoryToPlayFab()
    {
        if (!isLoggedIn)
        {
            Debug.LogError("Not logged in.");
            return;
        }

        List<ItemSaveData> itemsToSave = localInventory.GetItems();

        foreach (var item in itemsToSave)
        {
            if (string.IsNullOrEmpty(item.name) || string.IsNullOrEmpty(item.displayName))
            {
                Debug.LogWarning("Found an item with missing name or displayName.");
            }
        }

        string json = JsonUtility.ToJson(new ItemListWrapper { items = itemsToSave });

        Debug.Log("Saving Inventory JSON: " + json);

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "inventory", json } }
        },
        result => Debug.Log("Inventory saved."),
        error => Debug.LogError("Error saving inventory: " + error.GenerateErrorReport()));
    }

    public void LoadInventoryFromPlayFab()
    {
        if (!PlayFabLogin.IsLoggedIn)
        {
            Debug.LogError("Not logged in.");
            return;
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            result =>
            {
                if (result == null || result.Data == null || !result.Data.ContainsKey("inventory"))
                {
                    Debug.LogWarning("No inventory data found.");
                    return;
                }

                string json = result.Data["inventory"].Value;
                Debug.Log("Loaded Inventory JSON: " + json);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning("Inventory JSON is empty.");
                    return;
                }

                ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(json);
                if (wrapper == null || wrapper.items == null)
                {
                    Debug.LogError("Failed to parse inventory data.");
                    return;
                }

                ClearAllSlots();

                foreach (ItemSaveData saveData in wrapper.items)
                {
                    Item item = FindItemByName(saveData.name);
                    if (item != null)
                    {
                        InventoryItem inventoryItem = Instantiate(itemPrefab);
                        inventoryItem.Initialize(item, null);

                        if (inventorySlots.Length > 0)
                        {
                            inventorySlots[0].SetItem(inventoryItem);
                        }
                        else if (hotbarSlots.Length > 0)
                        {
                            hotbarSlots[0].SetItem(inventoryItem);
                        }
                        else if (equipmentSlots.Length > 0)
                        {
                            equipmentSlots[0].SetItem(inventoryItem);
                        }
                        else
                        {
                            Debug.LogWarning("Too many items to fit in the available slots.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Item not found: " + saveData.name);
                    }
                }

                Debug.Log("Inventory loaded.");
            },
            error => Debug.LogError("Error loading inventory: " + error.GenerateErrorReport()));
    }

    private void ClearAllSlots()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }
        foreach (var slot in hotbarSlots)
        {
            if (slot.myItem != null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }
        foreach (var slot in equipmentSlots)
        {
            if (slot.myItem != null)
            {
                Destroy(slot.myItem.gameObject);
                slot.myItem = null;
            }
        }
    }
    private Item FindItemByName(string itemName)
    {
        // Assuming you have a method to get an Item based on its name.
        foreach (var item in localInventory.GetItems())
        {
            if (item.name == itemName)
            {
                // Return the corresponding Item based on your ItemSaveData structure
                // If you have a way to get an Item from ItemSaveData, implement that here
                return ConvertToItem(item); // Update this method accordingly
            }
        }
        return null;
    }

    // Convert ItemSaveData to Item
    private Item ConvertToItem(ItemSaveData itemSaveData)
    {
        // Assuming you have a method to create or retrieve an Item instance based on ItemSaveData
        // Replace this logic with your actual implementation
        Item item = new Item
        {
            name = itemSaveData.name,
            displayName = itemSaveData.displayName,
            // Assign other properties as necessary
        };
        return item;
    }

}
