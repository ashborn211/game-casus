using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;
    public bool isEmpty = true;
    private bool isHovered = false;

    [Header("Sprite Renderer")]
    public GameObject spriteRenderer;

    [Header("Button")]
    public Button button;

    public bool AddItem(Item newItem)
    {
        if (!isEmpty)
        {
            Debug.LogError("Slot already contains an item!");
            return false;
        }

        item = newItem;
        isEmpty = false;

        Debug.Log("Item added to slot: " + newItem.name);
        SetSprite();
        return true;
    }

    public void RemoveItem()
    {
        if (item == null)
        {
            Debug.LogWarning($"[InventorySlot] Cannot remove item: Slot {gameObject.name} is already empty.");
            return;
        }

        Debug.Log($"[InventorySlot] Item removed: {item.name} from slot {gameObject.name}");
        item = null;
        isEmpty = true;
        SetSprite();
    }

    public void SetSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning($"[InventorySlot] spriteRenderer is not assigned on slot {gameObject.name}.");
            return;
        }

        Debug.Log($"[InventorySlot] Updating sprite for slot {gameObject.name}...");

        Image imageComponent = spriteRenderer.GetComponent<Image>();
        if (imageComponent != null)
        {
            if (item != null && item.sprite != null)
            {
                imageComponent.sprite = item.sprite;
                imageComponent.color = Color.white;
                Debug.Log($"[InventorySlot] Sprite set to {item.sprite.name} on slot {gameObject.name}");
            }
            else
            {
                imageComponent.sprite = null;
                imageComponent.color = new Color(1, 1, 1, 0);
                Debug.Log($"[InventorySlot] Sprite cleared on slot {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError($"[InventorySlot] No Image component found on spriteRenderer for slot {gameObject.name}");
        }
    }

    public void AddHighlight()
    {
        button.image.color = Color.yellow;
    }

    public void RemoveHighlight()
    {
        button.image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public bool IsHovered()
    {
        return isHovered;
    }

    void Start()
    {
        SetSprite();
    }
}