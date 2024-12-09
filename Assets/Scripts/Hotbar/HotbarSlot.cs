using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HotbarSlot : InventorySlot, IPointerClickHandler
{
    public bool isSelected = false;
    public GameObject hotbarFrame;

    public void SetActiveSlot(bool selected)
    {
        isSelected = selected;
        hotbarFrame.GetComponent<RawImage>().color = selected ? Color.yellow : Color.white;
    }

    public bool AddItemToHotbar(Item newItem)
    {
        if (item != null)
        {
            Debug.Log("Hotbar slot already contains an item.");
            return false;
        }

        item = newItem;
        isEmpty = false;
        SetSprite();
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Hotbar hotbar = FindObjectOfType<Hotbar>();
            if (hotbar != null)
            {
                int slotIndex = System.Array.IndexOf(hotbar.hotbar, gameObject);
                if (slotIndex >= 0)
                {
                    hotbar.RemoveItemFromHotbar(slotIndex);
                }
            }
        }
    }

    public void SetSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning($"[HotbarSlot] spriteRenderer is not assigned on slot {gameObject.name}.");
            return;
        }

        Image imageComponent = spriteRenderer.GetComponent<Image>();
        if (imageComponent != null)
        {
            if (item != null && item.sprite != null)
            {
                imageComponent.sprite = item.sprite;
                imageComponent.color = Color.white;
                Debug.Log($"[HotbarSlot] Sprite set to {item.sprite.name} on slot {gameObject.name}");
            }
            else
            {
                imageComponent.sprite = null;
                imageComponent.color = new Color(1, 1, 1, 0);
                Debug.Log($"[HotbarSlot] Sprite cleared on slot {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError($"[HotbarSlot] No Image component found on spriteRenderer for slot {gameObject.name}");
        }
    }

    void Start()
    {
        SetActiveSlot(isSelected);
        SetSprite();
    }

    void Update()
    {
        SetActiveSlot(isSelected);
    }
}