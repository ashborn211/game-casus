using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string name;
    public string displayName;
    public string itemTag; // Or use the existing tag
    public Sprite sprite; // Assuming you have a sprite field
}
