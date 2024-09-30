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
        Debug.LogError("Attempted to initialize InventoryItem with a null Item.");
        return;
    }

    // Get the asset path of the item
    string assetPath = AssetDatabase.GetAssetPath(item);

    // Check if the item is a .asset file
    if (!assetPath.EndsWith(".asset"))
    {
        Debug.LogError("Attempted to initialize with a non-asset Item: " + item.name);
        return; // Prevent initializing with a non-asset Item
    }

    activeSlot = parent;
    activeSlot.myItem = this;
    myItem = item;
    itemIcon.sprite = item.sprite;

    // Log the asset path of the item
    Debug.Log($"Initialized InventoryItem: {item.name} from path: {assetPath}");
}


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
