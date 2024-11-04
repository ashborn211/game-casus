// using UnityEngine;
// using UnityEngine.UI; // Make sure to include this for UI components
//
// public class HotbarSelector : MonoBehaviour
// {
//     private InventorySlot[] hotbarSlots; // Reference to hotbar slots
//     private int currentHotbarIndex = 0; // Track the currently selected hotbar slot
//     private Color selectedColor = Color.blue; // Light color for highlighting
//     private Color unselectedColor = Color.white; // Default color for unselected slots
//
//     private void Start()
//     {
//         // Automatically find all Inventory_Slot components in the children of Hotbar Slots
//         hotbarSlots = GetComponentsInChildren<InventorySlot>();
//
//         // Ensure that there are hotbar slots available
//         if (hotbarSlots.Length > 0)
//         {
//             UpdateHotbarSelection(); // Initialize selection
//         }
//         else
//         {
//             Debug.LogWarning("No Inventory_Slot components found in Hotbar Slots.");
//         }
//     }
//
//     private void Update()
//     {
//         // Change selection with scroll wheel
//         float scroll = Input.GetAxis("Mouse ScrollWheel");
//         if (scroll > 0f)
//         {
//             SelectNextHotbarSlot(1); // Scroll up
//         }
//         else if (scroll < 0f)
//         {
//             SelectNextHotbarSlot(-1); // Scroll down
//         }
//     }
//
//     private void SelectNextHotbarSlot(int direction)
//     {
//         currentHotbarIndex += direction;
//
//         // Wrap around if out of bounds
//         if (currentHotbarIndex < 0)
//         {
//             currentHotbarIndex = hotbarSlots.Length - 1;
//         }
//         else if (currentHotbarIndex >= hotbarSlots.Length)
//         {
//             currentHotbarIndex = 0;
//         }
//
//         UpdateHotbarSelection();
//     }
//
//     private void UpdateHotbarSelection()
//     {
//         // Update the visual indication of selected slot
//         for (int i = 0; i < hotbarSlots.Length; i++)
//         {
//             // Access the InventorySlot component
//             InventorySlot inventorySlot = hotbarSlots[i].GetComponent<InventorySlot>();
//
//             // Ensure the InventorySlot component is found
//             if (inventorySlot == null)
//             {
//                 Debug.LogError($"No InventorySlot component found on GameObject at index {i}. Please check your prefab.");
//                 continue; // Skip this iteration if InventorySlot component is not found
//             }
//
//             Image slotImage = inventorySlot.GetComponent<Image>();
//
//             // Ensure the Image component is found
//             if (slotImage == null)
//             {
//                 Debug.LogError($"No Image component found on InventorySlot at index {i}. Please check your prefab.");
//                 continue; // Skip this iteration if Image component is not found
//             }
//
//             if (i == currentHotbarIndex)
//             {
//                 // Highlight selected slot
//                 slotImage.color = selectedColor;
//
//                 // Scale up the selected slot
//                 hotbarSlots[i].transform.localScale = Vector3.one * 1.2f; // Adjust the multiplier as needed
//
//                 // Log the selected slot and item
//                 InventoryItem selectedItem = inventorySlot.myItem;
//                 Debug.Log($"Selected Slot: {i + 1}, Item: {(selectedItem != null ? selectedItem.myItem.name : "None")}");
//             }
//             else
//             {
//                 // Reset the color of unselected slots
//                 slotImage.color = unselectedColor;
//
//                 // Reset the scale of unselected slots
//                 hotbarSlots[i].transform.localScale = Vector3.one; // Reset to original scale
//             }
//         }
//     }
// }
