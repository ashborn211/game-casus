using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{

    public GameObject inventory;
    public GameObject[] hotbar;
    public int selectedSlot = 0;

	public void SetSelectedSlot(int slot)
	{
		SetSlotActive(selectedSlot, false);
		selectedSlot = slot;
		SetSlotActive(selectedSlot, true);
	}

	public void SetSlotActive(int slot, bool active)
    {
        hotbar[slot].GetComponent<HotbarSlot>().SetActiveSlot(active);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		// check if inventory is open
		if (!inventory.GetComponent<Inventory>().InventoryOpen)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Debug.Log("Selected Slot 1");
				SetSelectedSlot(0);
			}
        	if (Input.GetKeyDown(KeyCode.Alpha2))
			{
            	Debug.Log("Selected Slot 2");
				SetSelectedSlot(1);
        	}
			if (Input.GetKeyDown(KeyCode.Alpha3))
        	{
            	Debug.Log("Selected Slot 3");
				SetSelectedSlot(2);
        	}
			if (Input.GetKeyDown(KeyCode.Alpha4))
        	{
            	Debug.Log("Selected Slot 4");
				SetSelectedSlot(3);
        	}
        	if (Input.GetKeyDown(KeyCode.Alpha5))
        	{
            	Debug.Log("Selected Slot 5");
				SetSelectedSlot(4);
        	}
		}
    }

}
