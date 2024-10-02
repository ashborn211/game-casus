using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Inventory chestInventory; // Reference to the chest's inventory
    public int numberOfItems = 5; // Number of random items in the chest

    private bool isPlayerNearby = false;
    private bool isOpen = false;

    [SerializeField] private GameObject interactText; // UI Text to show "Press [E] to open"

    void Start()
    {
        // Add random items to the chest inventory
        for (int i = 0; i < numberOfItems; i++)
        {
            chestInventory.SpawnInventoryItem();
        }
    }

    void Update()
    {
        // Show "Press [E]" if player is nearby and chest is not open
        if (isPlayerNearby && !isOpen)
        {
            interactText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenChest();
            }
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    private void OpenChest()
    {
        isOpen = true;
        interactText.SetActive(false);
        
        // Show both player and chest inventories
        InventoryUIManager.Instance.ShowPlayerAndChestInventories(chestInventory);
    }

    private void CloseChest()
    {
        isOpen = false;
        InventoryUIManager.Instance.HidePlayerAndChestInventories();
    }

    // Trigger for detecting if the player is near the chest
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
