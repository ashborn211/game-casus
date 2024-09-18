using UnityEngine;

public class InventoryHotbarManager : MonoBehaviour
{
    public GameObject inventory; // Reference to the Inventory GameObject (should be a child of the main canvas)
    public GameObject hotbar; // Reference to the Hotbar GameObject (should be a child of the main canvas)

    // Offsets for positioning the hotbar
    public Vector2 hotbarPositionWhenInventoryClosed = new Vector2(-85f, -310f); // Position for when the inventory is closed
    public Vector2 hotbarPositionWhenInventoryOpen = new Vector2(-85f, -80f); // Position for when the inventory is open

    private RectTransform hotbarRectTransform;

    void Start()
    {
        hotbarRectTransform = hotbar.GetComponent<RectTransform>();

        if (inventory != null && hotbar != null)
        {
            // Initialize the hotbar position
            UpdateHotbarPosition();
        }
        else
        {
            Debug.LogWarning("Inventory or Hotbar is not assigned in the InventoryHotbarManager script.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Assuming you toggle inventory with 'E'
        {
            UpdateHotbarPosition();
        }
    }

    void UpdateHotbarPosition()
    {
        if (inventory.activeSelf)
        {
            // Inventory is open, place hotbar in the desired position
            hotbarRectTransform.anchoredPosition = hotbarPositionWhenInventoryOpen;
        }
        else
        {
            // Inventory is closed, place hotbar in the bottom-left corner
            hotbarRectTransform.anchoredPosition = hotbarPositionWhenInventoryClosed;
        }
    }
}
