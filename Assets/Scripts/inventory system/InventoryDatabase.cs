using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Inventory Database")]
public class InventoryDatabase : ScriptableObject
{
    public List<ItemSaveData> items = new List<ItemSaveData>();

    public void AddItem(ItemSaveData item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemSaveData item)
    {
        items.Remove(item);
    }

    public void SetItems(List<ItemSaveData> newItems)
    {
        items = newItems;
    }

    public List<ItemSaveData> GetItems()
    {
        return items;
    }
}
