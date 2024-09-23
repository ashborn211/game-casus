using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            SetItem(Inventory.carriedItem);
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
            Destroy(myItem.gameObject);
        }

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;
    }
}
