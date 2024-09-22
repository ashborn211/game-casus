using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemTag;
    private Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }
    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();

        if (itemIcon == null)
        {
            Debug.LogError("Item Icon is missing on " + gameObject.name);
        }
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        if (item == null)
        {
            Debug.LogError("Item is null.");
            return;
        }

        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;

        if (itemIcon != null)
        {
            itemIcon.sprite = item.sprite;
        }
        canvasGroup.blocksRaycasts = true; // Ensure raycasts are blocked when in inventory
    }

    // Click handling (Left Click)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
        // You can add right-click handling here if needed
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Show a context menu or use the item, etc.
            Debug.Log("Right-clicked on item: " + myItem.name);
        }
    }

    // Drag handling
    public void OnBeginDrag(PointerEventData eventData)
    {
        Inventory.Singleton.SetCarriedItem(this); // Start carrying the item
        canvasGroup.blocksRaycasts = false; // Allow the item to be dragged over other UI elements
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Inventory.Singleton.carriedItem == this)
        {
            transform.position = Input.mousePosition; // Follow the mouse during drag
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Enable raycast blocking when drag ends
        // You can add additional behavior here, such as checking if the item was dropped on a valid slot
    }
}
