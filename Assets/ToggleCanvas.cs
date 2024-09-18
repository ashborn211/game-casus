using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    // Reference to the Inventory GameObject (parent of inventory slots and equipment slots)
    public GameObject inventory;

    // Reference to the Hotbar GameObject (parent of hotbar slots)
    public GameObject hotbar;

    // Key to toggle the Inventory (set to E key)
    private KeyCode toggleKey = KeyCode.E;

    void Start()
    {
        if (hotbar != null)
        {
            hotbar.SetActive(true);  // Ensure hotbar is visible at the start
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
                inventory.SetActive(!inventory.activeSelf);
            }
            else
            {
                Debug.LogWarning("Inventory panel is not assigned in the ToggleCanvas script.");
            }
        }
    }
}
