using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem
{

    public event UnityAction OnInventoryChanged;
    public event UnityAction<ItemData, int> OnInventoryFull;

    [SerializeField] private int size = 15;

    public int Size => size;

    [SerializeField] private List<InventorySlot> slots;

    public List<InventorySlot> InventorySlots => slots;

    [ContextMenu(itemName: "Adjust Size")]

    private void Start()
    {
        if (slots == null)
        {
            AdjustSize();
        }

        AdjustSize();
    }



    private void AdjustSize()
    {
        if (slots == null) slots = new List<InventorySlot>();

        // Remove extra slots if the list exceeds the size
        if (slots.Count > size)
        {
            slots.RemoveRange(size, slots.Count - size);
        }

        // Add new slots if the list is smaller than the desired size
        while (slots.Count < size)
        {
            // Ensure every slot is initialized as a new InventorySlot
            InventorySlot newSlot = new InventorySlot();
            slots.Add(newSlot);
        }
    }


    private InventorySlot FindSlot(ItemData itemData)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.ItemData == itemData && slot.CanAddStackSize())
            {
                return slot;
            }
        }
        return null;
    }

    private bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = slots.FirstOrDefault(slot => slot.IsEmptySlot());

        return freeSlot != null;
    }

    public bool CanAddItem(ItemData itemData)
    {
        return FindSlot(itemData) != null || HasFreeSlot(out InventorySlot freeSlot);
    }

    public bool AddItem(ItemData itemData, int amount)
    {
        int tempAmount = amount;

        while (amount > 0)
        {
            InventorySlot slotContainsItem = FindSlot(itemData);

            if (slotContainsItem != null)
            {
                int remainingSpace = slotContainsItem.GetRemainingSpace();
                int amountToAdd = Mathf.Min(a: remainingSpace, b: amount);
                slotContainsItem.AddToStack(amountToAdd);
                amount -= amountToAdd;
            }
            else if (HasFreeSlot(out InventorySlot freeSlot))
            {
                int amountToAdd = Mathf.Min(a: itemData.GetMaxStackSize(), b: amount);
                freeSlot.UpdateInventorySlot(itemData, amountToAdd);
                amount -= amountToAdd;
            }
            else 
            {
                HandleInventoryFull(itemData, amountAdded: tempAmount - amount, amountRemaining: amount);
                return false;
            }
        }
        OnInventoryChanged?.Invoke();
        return true;
    }
    
    private void HandleInventoryFull(ItemData itemData, int amountAdded, int amountRemaining)
    {
        Debug.Log(message:$"Added {amountAdded} {itemData.GetItemName()}");

        OnInventoryFull?.Invoke(itemData, amountRemaining);
        OnInventoryChanged?.Invoke();
    }


    public void AddToSpecificSlot(InventorySlot slot, int amount)
    {
        slot.AddToStack(amount);
    }











}
