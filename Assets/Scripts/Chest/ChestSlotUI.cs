using UnityEngine;
using UnityEngine.UI;

public class ChestSlotUI : MonoBehaviour
{
    public Image itemImage; // Reference to the image component
    public Text itemNameText; // Reference to the item name text component

    public void SetItem(InventoryItem item)
    {
        itemImage.sprite = item.myItem.icon; // Assuming InventoryItem has a reference to its icon
        itemNameText.text = item.myItem.name;
        itemImage.gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        itemImage.sprite = null;
        itemNameText.text = "";
        itemImage.gameObject.SetActive(false);
    }
}
