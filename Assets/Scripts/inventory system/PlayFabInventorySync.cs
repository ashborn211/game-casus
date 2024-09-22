using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;

public class PlayFabInventorySync : MonoBehaviour
{
    public InventoryDatabase localInventory;
    private bool isLoggedIn = false;

    void Start()
    {
        if (localInventory == null)
        {
            Debug.LogError("LocalInventory is not assigned.");
            return;
        }

        // Start the coroutine to check login status
        StartCoroutine(CheckLoginStatus());
    }

    private IEnumerator CheckLoginStatus()
    {
        // Wait until the user is logged in
        while (!PlayFabLogin.IsLoggedIn)
        {
            Debug.Log("Waiting for login...");
            yield return new WaitForSeconds(1f); // Check every 1 second
        }

        // Set the flag to true once logged in
        isLoggedIn = true;
        Debug.Log("Logged in. Loading inventory.");
        LoadInventoryFromPlayFab();
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
        if (!isLoggedIn)
        {
            Debug.LogError("Not logged in. Cannot save inventory.");
            return;
        }

        List<ItemSaveData> itemsToSave = localInventory.GetItems();
        string json = JsonUtility.ToJson(new ItemListWrapper { items = itemsToSave });
        Debug.Log("Inventory JSON to save: " + json);

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                { "inventory", json }
            }
        },
        result =>
        {
            Debug.Log("Inventory saved to PlayFab.");
        },
        error =>
        {
            Debug.LogError("Error saving inventory: " + error.GenerateErrorReport());
        });
    }

    public void LoadInventoryFromPlayFab()
    {
        if (!isLoggedIn)
        {
            Debug.LogError("Not logged in. Cannot load inventory.");
            return;
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            if (result.Data != null && result.Data.ContainsKey("inventory"))
            {
                string json = result.Data["inventory"].Value;
                ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(json);
                localInventory.SetItems(wrapper.items);
                Debug.Log("Inventory loaded from PlayFab.");
                Debug.Log("Inventory JSON loaded: " + json);
            }
        },
        error => Debug.LogError("Error loading inventory: " + error.GenerateErrorReport()));
    }
}