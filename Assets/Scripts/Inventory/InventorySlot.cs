using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    public GameObject item;
    public bool isEmpty = true;

    public void AddItem(GameObject item)
    {
        if (!isEmpty)
        {
            return;
        }
        this.item = item;
        isEmpty = false;
    }

    public void RemoveItem()
    {
        if (isEmpty)
        {
            return;
        }
        this.item = null;
        isEmpty = true;
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
