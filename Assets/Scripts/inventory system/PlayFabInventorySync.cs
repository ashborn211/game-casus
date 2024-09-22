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

        // Ensure item name, displayName, and tag are properly set
        if (string.IsNullOrEmpty(item.name) || string.IsNullOrEmpty(item.displayName))
        {
            Debug.LogError("Item data is invalid.");    
            return;
        }

        // Populate ItemSaveData properly
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

        // Check for item validity before saving
        foreach (var item in itemsToSave)
        {
            if (string.IsNullOrEmpty(item.name) || string.IsNullOrEmpty(item.displayName))
            {
                Debug.LogWarning("Found an item with missing name or displayName.");
            }
        }

        string json = JsonUtility.ToJson(new ItemListWrapper { items = itemsToSave });

        Debug.Log("Saving Inventory JSON: " + json); // Log JSON data

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
                Debug.Log("Loaded Inventory JSON: " + json); // Log JSON data

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

                int inventoryIndex = 0;
                int hotbarIndex = 0;
                int equipmentIndex = 0;

                foreach (ItemSaveData saveData in wrapper.items)
                {
                    Item item = FindItemByName(saveData.name);
                    if (item != null)
                    {
                        InventoryItem inventoryItem = Instantiate(itemPrefab);
                        inventoryItem.Initialize(item, null);

                        if (inventoryIndex < inventorySlots.Length)
                        {
                            inventorySlots[inventoryIndex].SetItem(inventoryItem);
                            inventoryIndex++;
                        }
                        else if (hotbarIndex < hotbarSlots.Length)
                        {
                            hotbarSlots[hotbarIndex].SetItem(inventoryItem);
                            hotbarIndex++;
                        }
                        else if (equipmentIndex < equipmentSlots.Length)
                        {
                            equipmentSlots[equipmentIndex].SetItem(inventoryItem);
                            equipmentIndex++;
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
        foreach (var item in Inventory.Singleton.GetItems())
        {
            if (item.name == itemName)
            {
                return item;
            }
        }
        return null;
    }
}