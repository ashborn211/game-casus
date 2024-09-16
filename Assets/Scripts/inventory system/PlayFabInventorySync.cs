using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayFabInventorySync : MonoBehaviour
{
    public InventoryDatabase localInventory;

    void Start()
    {
        LoadInventoryFromPlayFab();
    }

    public void AddItemToInventory(Item item, int qty)
    {
        ItemSaveData newItem = new ItemSaveData(item.name, item.name, qty, item.itemTag.ToString());
        localInventory.AddItem(newItem);
        SaveInventoryToPlayFab();
    }

    public void SaveInventoryToPlayFab()
    {
        List<ItemSaveData> itemsToSave = localInventory.GetItems();
        string json = JsonUtility.ToJson(new ItemListWrapper { items = itemsToSave });

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() { { "inventory", json } }
        },
        result => Debug.Log("Inventory saved to PlayFab."),
        error => Debug.LogError("Error saving inventory: " + error.GenerateErrorReport()));
    }

    public void LoadInventoryFromPlayFab()
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
}
