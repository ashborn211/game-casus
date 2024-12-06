using UnityEngine;

public class PickUpGold : MonoBehaviour
{
    public Item goldScriptableObject; // Reference to the Gold ScriptableObject

    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory script not found in the scene!");
        }
    }

    void OnMouseDown()
    {
        // Check if the Gold ScriptableObject and Inventory are assigned
        if (goldScriptableObject != null && inventory != null)
        {
            // Add the gold amount from the ScriptableObject to the player's inventory
            inventory.AddGold(goldScriptableObject.amount);
            Debug.Log("Gold Added: " + goldScriptableObject.amount);
        }
        else
        {
            Debug.LogWarning("Gold ScriptableObject or Inventory is not assigned or found!");
        }

        // Destroy the gold object after it has been picked up
        Destroy(gameObject);
    }
}
