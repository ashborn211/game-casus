using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [SerializeField] private GameObject playerInventoryUI;
    [SerializeField] private GameObject chestInventoryUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Show both player and chest inventories
    public void ShowPlayerAndChestInventories(Inventory chestInventory)
    {
        playerInventoryUI.SetActive(true);
        chestInventoryUI.SetActive(true);

        // Populate chest inventory UI with chest's items
        PopulateChestInventoryUI(chestInventory);
    }

    // Hide both player and chest inventories
    public void HidePlayerAndChestInventories()
    {
        playerInventoryUI.SetActive(false);
        chestInventoryUI.SetActive(false);
    }

    private void PopulateChestInventoryUI(Inventory chestInventory)
    {
        // Logic to display the chest's items in the UI
        InventorySlot[] chestSlots = chestInventory.GetInventorySlots();

        for (int i = 0; i < chestSlots.Length; i++)
        {
            // Assuming you have a ChestSlotUI that mirrors the InventorySlotUI
            ChestSlotUI chestSlotUI = chestInventoryUI.transform.Find("ChestInventoryPanel/ChestScreen/ChestSlots/ChestSlotUI" + (i + 1)).GetComponent<ChestSlotUI>();
            if (chestSlots[i].myItem != null)
            {
                chestSlotUI.SetItem(chestSlots[i].myItem);
            }
            else
            {
                chestSlotUI.ClearItem();
            }
        }
    }
}
