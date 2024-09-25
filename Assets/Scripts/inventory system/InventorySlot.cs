using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class InventorySlot
{
    [SerializeField] private ItemData itemData;

    public ItemData ItemData => itemData;

    [SerializeField] private int stackSize;

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
        return stackSize < itemData.GetMaxStackSize();
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

    public bool SplitStack(out int splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = 0;
            return false;    
        }

        splitStack = Mathf.RoundToInt(f: (float)stackSize / 2);
        return true;
    }

    public bool IsEmptySlot()
    {
        return itemData == null;
    }






}
