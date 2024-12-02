using System.Collections.Generic;
using UnityEngine;
using Kellojo.SimpleLootTable;  // Ensure the LootTable namespace is correct

public class EnemyDrop : MonoBehaviour
{
    public List<Transform> DropSlots;    // Slots where the drops will be instantiated
    public GameObjectLootTable LootTable; // Reference to the loot table (e.g., crab)

    public void ItemDrop()
    {
        // Ensure the LootTable is assigned in the Inspector
        if (LootTable != null)
        {
            // Spawn guaranteed drops
            var guaranteedDrops = LootTable.GetGuaranteedDrops();
            foreach (var drop in guaranteedDrops)
            {
                // Instantiate the drop GameObject at the enemy's position and rotation
                Instantiate(drop, transform.position, transform.rotation);
            }

            // Spawn optional drops (example: 2 optional drops)
            var optionalDrops = LootTable.GetOptionalDrops(2);
            foreach (var drop in optionalDrops)
            {
                Instantiate(drop, transform.position, transform.rotation);
            }

            // Spawn both guaranteed and optional drops (example: 2 items)
            var combinedDrops = LootTable.GetGuaranteedAndOptionalDrops(2);
            foreach (var drop in combinedDrops)
            {
                Instantiate(drop, transform.position, transform.rotation);
            }
        }
        else
        {
            Debug.LogError("LootTable is not assigned!");
        }
    }
}
