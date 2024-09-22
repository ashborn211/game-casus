using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public string myTag;
    public InventoryItem myItem { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.Singleton == null)
            {
                Debug.LogError("Inventory.Singleton is not initialized.");
                return;
            }

            if (Inventory.Singleton.carriedItem == null) // Corrected reference
            {
                Debug.LogWarning("No carried item to place in the slot.");
                return;
            }

            SetItem(Inventory.Singleton.carriedItem); // Corrected reference
        }
    }

    public void SetItem(InventoryItem item)
    {
        if (item == null)
        {
            Debug.LogError("Trying to set a null item.");
            return;
        }

        if (myItem != null)
        {
            // Destroy existing item
            Destroy(myItem.gameObject);
        }

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        // Assuming EquipEquipment is a valid method
        Inventory.Singleton.EquipEquipment(myTag, myItem); // Corrected reference
    }
}
