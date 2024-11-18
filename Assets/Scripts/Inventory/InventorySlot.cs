using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    public Item item = null;
    public bool isEmpty = true;

    [Header("Sprite Renderer")]
    public GameObject spriteRenderer;

    //Item newItem
    public void AddItem()
    {
        Debug.Log("Adding item to slot...");
        // if (item != null)
        // {
        //     Debug.Log("Slot already contains an item!");
        //     return;
        // }
        // item = newItem;
    }

    public void RemoveItem()
    {
        if (item == null)
        {
            Debug.Log("Slot is empty!");
            return;
        }
        item = null;
    }

    public void SetSprite()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
