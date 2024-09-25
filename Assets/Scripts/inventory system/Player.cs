using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private ItemData itemtest;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<InventorySystem>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            inventory.AddItem(itemtest, amount: 2);
        }
    }
}
