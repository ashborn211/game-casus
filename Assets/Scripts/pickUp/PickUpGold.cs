using UnityEngine;

public class PickUpGold : MonoBehaviour
{
    public int goldAmount = 1;  // Amount of gold to add when picked up

    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.Find("inventory").GetComponent<Inventory>();
    }

    void OnMouseDown()
    {
        if (inventory != null)
        {
            inventory.AddGold(goldAmount);
            Debug.Log("Gold Added: " + goldAmount);
        }

        Destroy(gameObject);
    }
}
