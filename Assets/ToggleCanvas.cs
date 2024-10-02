using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    // Reference to the Inventory GameObject (parent of inventory slots and equipment slots)
    public GameObject inventory;

    // Reference to the Hotbar GameObject (parent of hotbar slots)
    public GameObject hotbar;

    // Key to toggle the Inventory (set to E key)
    private KeyCode toggleKey = KeyCode.E;

    // Reference to the RectTransform of the hotbar
    private RectTransform hotbarRectTransform;

    void Start()
    {
        if (hotbar != null)
        {
            hotbar.SetActive(true);  // Ensure hotbar is visible at the start
            hotbarRectTransform = hotbar.GetComponent<RectTransform>(); // Get the RectTransform component
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Toggle the active state of the Inventory
            if (inventory != null)
            {
                bool isActive = !inventory.activeSelf;
                inventory.SetActive(isActive);
                PositionHotbar(isActive);
            }
            else
            {
                Debug.LogWarning("Inventory panel is not assigned in the ToggleCanvas script.");
            }
        }
    }

    private void PositionHotbar(bool inventoryVisible)
    {
        if (hotbarRectTransform != null)
        {
            if (inventoryVisible)
            {
                // Position the hotbar at the middle bottom of the inventory
                hotbarRectTransform.anchoredPosition = new Vector2(0, -140); // Adjust Y value as needed
            }
            else
            {
                // Position the hotbar at the bottom right corner of the canvas
                hotbarRectTransform.anchoredPosition = new Vector2(-500, -250); // Adjust X and Y values as needed
            }
        }
    }
}
