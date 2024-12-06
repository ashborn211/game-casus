using UnityEngine;

public class PickUpGold : MonoBehaviour
{
    public Item goldScriptableObject; 

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
        if (goldScriptableObject != null && inventory != null)
        {
            inventory.AddGold(goldScriptableObject.amount);
            Debug.Log("Gold Added: " + goldScriptableObject.amount);
        }
        else
        {
            Debug.LogWarning("Gold ScriptableObject or Inventory is not assigned or found!");
        }

        Destroy(gameObject);
    }
}
