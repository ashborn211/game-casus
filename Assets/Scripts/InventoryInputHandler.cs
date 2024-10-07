using UnityEngine;

public class InventoryInputHandler : MonoBehaviour
{
    void Update()
    {
        // Press 'S' to manually save the inventory
        if (Input.GetKeyDown(KeyCode.G))
        {
            Inventory.Singleton.SaveInventory();
        }

        // Press 'L' to manually load the inventory
        if (Input.GetKeyDown(KeyCode.L))
        {
            Inventory.Singleton.LoadInventory();
        }
    }
}
