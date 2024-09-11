using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    // Reference to the Inventory GameObject (parent of inventory slots and equipment slots)
    public GameObject inventory;

    // Reference to the Hotbar GameObject (parent of hotbar slots)
    public GameObject hotbar;

    // Key to toggle the Inventory (set to E key)
    private KeyCode toggleKey = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Check if the inventory panel is assigned
            if (inventory != null)
            {
                // Toggle the active state of the Inventory, leaving the Hotbar visible
                inventory.SetActive(!inventory.activeSelf);

                // Ensure the Hotbar remains visible regardless of inventory state
                if (hotbar != null)
                {
                    hotbar.SetActive(true);  // Always keep the hotbar visible
                }
            }
            else
            {
                Debug.LogWarning("Inventory panel is not assigned in the ToggleCanvas script.");
            }
        }
    }
}
