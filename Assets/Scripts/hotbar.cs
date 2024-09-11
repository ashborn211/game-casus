using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryHotbarController : MonoBehaviour
{
    public Canvas inventoryCanvas;  // Reference to the Inventory Canvas
    public Canvas hotbarCanvas;     // Reference to the Hotbar Canvas
    public RectTransform hotbar;    // Reference to the Hotbar's RectTransform
    private bool inventoryOpen = true; // Tracks whether the inventory is open

    void Update()
    {
        // Check for inventory toggle input (example, 'I' key)
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;

        if (inventoryOpen)
        {
            // When the inventory is open, show the inventory canvas
            inventoryCanvas.gameObject.SetActive(true);
            // Set hotbar size to full size
            hotbar.localScale = Vector3.one;
        }
        else
        {
            // When the inventory is closed, hide the inventory canvas
            inventoryCanvas.gameObject.SetActive(false);
            // Keep hotbar visible but shrink it
            hotbar.localScale = new Vector3(0.5f, 0.5f, 0.5f);  // Adjust this to the size you want
        }
    }
}
