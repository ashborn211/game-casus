using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayFabInventorySync : MonoBehaviour
{
    public InventoryDatabase localInventory;

    void Start()
    {
        if (localInventory == null)
        {
            Debug.LogError("LocalInventory is not assigned.");
            return;
        }

        // Use the updated IsLoggedIn property
        if (PlayFabLogin.IsLoggedIn)
        {
            LoadInventoryFromPlayFab();
        }
        else
        {
            Debug.Log("Not logged in yet.");
            // Optionally handle this scenario, e.g., retry after some delay
        }
    }

    public void AddItemToInventory(Item item, int qty)
    {
        if (item == null)
        {
            Debug.LogError("Item is null.");
            return;
        }

        if (localInventory == null)
        {
            Debug.LogError("Local inventory is null.");
            return;
        }

        ItemSaveData newItem = new ItemSaveData(item.name, item.name, qty, item.itemTag.ToString());
        localInventory.AddItem(newItem);
        SaveInventoryToPlayFab();
    }

    public void SaveInventoryToPlayFab()
    {
        List<ItemSaveData> itemsToSave = localInventory.GetItems();
        string json = JsonUtility.ToJson(new ItemListWrapper { items = itemsToSave });
        Debug.Log("Inventory JSON to save: " + json);

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                { "inventory", json }
            }
        },
        result => {
            Debug.Log("Inventory saved to PlayFab.");
        },
        error => {
            Debug.LogError("Error saving inventory: " + error.GenerateErrorReport());
        });
    }

    public void LoadInventoryFromPlayFab()
    {
        if (PlayFabLogin.IsLoggedIn)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            result => {
                if (result.Data != null && result.Data.ContainsKey("inventory"))
                {
                    string json = result.Data["inventory"].Value;
                    ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(json);
                    localInventory.SetItems(wrapper.items);
                }
            },
            error => Debug.LogError("Error loading inventory: " + error.GenerateErrorReport()));
        }
        else
        {
            Debug.LogError("Not logged in. Cannot load inventory.");
        }
    }
}
