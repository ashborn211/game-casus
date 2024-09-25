using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "SO/ItemData")]
public class ItemData : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField][TextArea] private string description;
    [SerializeField] private int maxStackSize;
    [SerializeField] private Sprite icon;

    public string GetItemName() => itemName;

    public string GetDescription() => description;

    public int GetMaxStackSize() => maxStackSize;

    public Sprite GetIcon() => icon;

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrWhiteSpace(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {

    }





}
