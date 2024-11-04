// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
//
// public class InventorySlot : MonoBehaviour, IPointerClickHandler
// {
//     public InventoryItem myItem { get; set; }
//
//     // Use Item.ArmorType instead of a separate ArmorType enum
//     public Item.ArmorType armorType;
//
//     public void OnPointerClick(PointerEventData eventData)
//     {
//         if (eventData.button == PointerEventData.InputButton.Left)
//         {
//             if (Inventory.carriedItem == null) return;
//
//             // Ensure the carried item can be placed in this slot (matching armor type)
//             if (armorType != Item.ArmorType.None && Inventory.carriedItem.armorType != armorType) return;
//
//             SetItem(Inventory.carriedItem);
//         }
//     }
//
//     public void SetItem(InventoryItem item)
//     {
//         Inventory.carriedItem = null;
//
//         if (item.activeSlot != null)
//         {
//             item.activeSlot.myItem = null;
//         }
//
//         myItem = item;
//         myItem.activeSlot = this;
//         myItem.transform.SetParent(transform);
//         myItem.canvasGroup.blocksRaycasts = true;
//
//         if (armorType != Item.ArmorType.None)
//         {
//             Inventory.Singleton.EquipEquipment(armorType, myItem);
//         }
//     }
// }
