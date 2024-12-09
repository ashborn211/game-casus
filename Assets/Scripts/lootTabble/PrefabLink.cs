using UnityEngine;

public class PrefabLink : MonoBehaviour
{
    public Item linkedScriptableObject; // Reference to the Item ScriptableObject

    private void OnValidate()
    {
        // Automatically set the prefab reference in the ScriptableObject if it's missing
        if (linkedScriptableObject != null && linkedScriptableObject.equipmentPrefab == null)
        {
            linkedScriptableObject.equipmentPrefab = gameObject;
        }
    }
}
