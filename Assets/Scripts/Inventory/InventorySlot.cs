using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item = null;
    public bool isEmpty = true;

    [Header("Sprite Renderer")]
    public GameObject spriteRenderer;

    public void AddItem(Item newItem)
    {
        if (item != null)
        {
            Debug.LogError("Slot already contains an item!");
            return;
        }

        item = newItem;
        isEmpty = false;

        Debug.Log("Item added to slot: " + newItem.name);
        // Update the sprite here
        SetSprite();
    }

    public void RemoveItem()
    {
        if (item == null)
        {
            Debug.LogWarning($"[InventorySlot] Cannot remove item: Slot {gameObject.name} is already empty.");
            return;
        }

        //Clear the sprite here
        Debug.Log($"[InventorySlot] Item removed: {item.name} from slot {gameObject.name}");
        item = null;
        isEmpty = true;
        SetSprite();

    }

    //Method to update the sprite
    public void SetSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning($"[InventorySlot] spriteRenderer is not assigned on slot {gameObject.name}.");
            return; // Cannot update sprite without a renderer
        }

        Debug.Log($"[InventorySlot] Updating sprite for slot {gameObject.name}...");

        // Check if the spriteRenderer has a UI Image component
        Image imageComponent = spriteRenderer.GetComponent<Image>();
        if (imageComponent != null)
        {
            if (item != null && item.sprite != null)
            {
                imageComponent.sprite = item.sprite; // Set the sprite
                imageComponent.color = Color.white; //  sprite is visible
                Debug.Log($"[InventorySlot] Sprite set to {item.sprite.name} on slot {gameObject.name}");
            }
            else
            {
                imageComponent.sprite = null;
                imageComponent.color = new Color(1, 1, 1, 0); // Make it invisible
                Debug.Log($"[InventorySlot] Sprite cleared on slot {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError($"[InventorySlot] No Image component found on spriteRenderer for slot {gameObject.name}");
        }
    }

}
