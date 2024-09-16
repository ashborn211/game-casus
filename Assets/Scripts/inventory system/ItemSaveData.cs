using System;

[Serializable]
public class ItemSaveData
{
    public string name;
    public string displayName;
    public int quantity;
    public string tag;

    public ItemSaveData(string name, string displayName, int quantity, string tag)
    {
        this.name = name;
        this.displayName = displayName;
        this.quantity = quantity;
        this.tag = tag;
    }
}
