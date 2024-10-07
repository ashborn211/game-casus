using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        if (item == null)
        {
            Debug.Log("Attempted to initialize InventoryItem with a null Item.");
            return;
        }

        string assetPath = AssetDatabase.GetAssetPath(item);

        if (!assetPath.EndsWith(".asset"))
        {
            Debug.Log("Attempted to initialize with a non-asset Item: " + item.name);
            return; // Prevent initializing with a non-asset Item
        }

        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;

        Debug.Log($"Initialized InventoryItem: {item.name} from path: {assetPath}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }

    // Property to access the armor type from the Item class
    public Item.ArmorType armorType
    {
        get { return myItem != null ? myItem.armorType : Item.ArmorType.None; }
    }
}
