using System;

[Serializable]
public class InventorySlot
{
    public ItemData itemData; // Store item reference
    public int stackSize;     // Track the current stack size

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = 0;
    }

    public void UpdateInventorySlot(ItemData item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }

    public bool CanAddStackSize()
    {
        return itemData != null && stackSize < itemData.GetMaxStackSize();
    }

    public int GetRemainingSpace()
    {
        return itemData.GetMaxStackSize() - stackSize;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if (stackSize <= 0)
        {
            ClearSlot();
        }
    }

    public bool IsEmptySlot()
    {
        return itemData == null;
    }
}
