using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;

    [Header("Item Type")]
    public ItemType itemType; // Field for item type

    [Header("Armor Type (if applicable)")]
    public ArmorType armorType; // Field for specifying type of armor
    public ArmorMaterial armorMaterial; // Field for specifying the material of the armor

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;

    [Header("Validity Check")]
    public bool isValidItem = true; // Field for checking if the item is valid

    [Header("DMG")]
    public int damage; // Field for weapon damage

    [Header("Currency Amount")]
    public int amount; // Amount of currency, applicable only for currency items

    // Enum for item types
    public enum ItemType
    {
        Weapon,
        Tool,
        Collectible,
        Potion,
        Consumable,
        Currency,
        Armor // New item type for armor
    }

    // Enum for armor types
    public enum ArmorType { None, Helmet, Chestplate, Leggings, Boots }

    // Enum for armor materials
    public enum ArmorMaterial { None, Wood, Bronze, Iron, Steel }

    // Method to handle the use effect of the item
    public void UseEffect()
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                Debug.Log("Using weapon: " + name);
                break;
            case ItemType.Tool:
                Debug.Log("Using tool: " + name);
                break;
            case ItemType.Collectible:
                Debug.Log("Collecting: " + name);
                break;
            case ItemType.Potion:
                Debug.Log("Drinking potion: " + name);
                break;
            case ItemType.Consumable:
                Debug.Log("Using consumable: " + name);
                break;
            case ItemType.Currency:
                Debug.Log("Collecting currency: " + name + " Amount: " + amount);
                break;
            case ItemType.Armor:
                Debug.Log("Equipping armor: " + name + " Type: " + armorType + " Material: " + armorMaterial);
                break;
            default:
                Debug.LogWarning("Item type not handled: " + itemType);
                break;
        }
    }
}
