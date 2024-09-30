using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public List<ItemData> items = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public string itemName; // Name of the Scriptable Object
    public SlotTag itemTag; // Tag for the item slot

    public ItemData(string name, SlotTag tag)
    {
        itemName = name;
        itemTag = tag;
    }
}
