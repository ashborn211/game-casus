using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{

    public const int HOTBAR_SIZE = 5;

    public InventorySlot[] hotbar; // = new InventorySlot[HOTBAR_SIZE];
    public int selectedSlot = 0;

    // public void AddItem(GameObject item)
    // {
    //     hotbar[selectedSlot].AddItem(item);
    // }
    //
    // public void RemoveItem(GameObject item)
    // {
    //     hotbar[selectedSlot].RemoveItem();
    // }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
