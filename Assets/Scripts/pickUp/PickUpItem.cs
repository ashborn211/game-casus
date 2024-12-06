using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item itemToPickUp;  // Reference to the Item ScriptableObject (e.g., Sword)

    private Inventory inventory; // Reference to the Inventory script

    void Start()
    {
        // Try to find the Inventory script in the scene (or assign it manually)
        inventory = FindObjectOfType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("Inventory script not found in the scene!");
        }
    }

    void OnMouseDown()
    {
        // Check if the Inventory and Item are properly assigned
        if (inventory != null && itemToPickUp != null)
        {
            // Use the AddItem function from Inventory to add the item
            inventory.AddItem(itemToPickUp);
            Debug.Log(itemToPickUp + " added to inventory.");

            // Destroy the item prefab after it has been picked up
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Inventory or Item not assigned.");
        }
    }
}
