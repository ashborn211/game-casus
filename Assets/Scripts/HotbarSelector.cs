using UnityEngine;
using UnityEngine.UI; // Make sure to include this for UI components

public class HotbarManager : MonoBehaviour
{
    private InventorySlot[] hotbarSlots; // Reference to hotbar slots
    private int currentHotbarIndex = 0; // Track the currently selected hotbar slot
    private Color selectedColor = new Color(1f, 1f, 0.5f); // Light color for highlighting
    private Color unselectedColor = Color.white; // Default color for unselected slots

    private void Start()
    {
        // Automatically find all Inventory_Slot components in the children of Hotbar Slots
        hotbarSlots = GetComponentsInChildren<InventorySlot>();

        // Ensure that there are hotbar slots available
        if (hotbarSlots.Length > 0)
        {
            UpdateHotbarSelection(); // Initialize selection
        }
        else
        {
            Debug.LogWarning("No Inventory_Slot components found in Hotbar Slots.");
        }
    }

    private void Update()
    {
        // Change selection with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SelectNextHotbarSlot(1); // Scroll up
        }
        else if (scroll < 0f)
        {
            SelectNextHotbarSlot(-1); // Scroll down
        }
    }

    private void SelectNextHotbarSlot(int direction)
    {
        currentHotbarIndex += direction;

        // Wrap around if out of bounds
        if (currentHotbarIndex < 0)
        {
            currentHotbarIndex = hotbarSlots.Length - 1;
        }
        else if (currentHotbarIndex >= hotbarSlots.Length)
        {
            currentHotbarIndex = 0;
        }

        UpdateHotbarSelection();
    }

    private void UpdateHotbarSelection()
    {
        // Update the visual indication of selected slot, e.g., change color or highlight
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            Image slotImage = hotbarSlots[i].GetComponent<Image>();

            if (i == currentHotbarIndex)
            {
                // Highlight selected slot
                slotImage.color = selectedColor;

                // Log the selected slot and item
                InventoryItem selectedItem = hotbarSlots[i].myItem;
                Debug.Log($"Selected Slot: {i + 1}, Item: {(selectedItem != null ? selectedItem.myItem.name : "None")}");
            }
            else
            {
                // Reset the color of unselected slots
                slotImage.color = unselectedColor;
            }
        }
    }
}
