using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item itemToPickUp;  

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
        if (inventory != null && itemToPickUp != null)
        {
            inventory.AddItem(itemToPickUp);
            Debug.Log(itemToPickUp + " added to inventory.");

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Inventory or Item not assigned.");
        }
    }
}
