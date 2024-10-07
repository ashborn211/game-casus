using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;

    [Header("Validity Check")]
    public bool isValidItem = true; // Add this field

    [Header("DMG")]
    public int damage; // Add this field
}
