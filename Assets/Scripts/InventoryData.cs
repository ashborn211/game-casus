using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public List<ItemData> items = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Item.ItemType itemType; // Change this line

    public ItemData(string itemName, Item.ItemType itemType)
    {
        this.itemName = itemName;
        this.itemType = itemType;
    }
}


