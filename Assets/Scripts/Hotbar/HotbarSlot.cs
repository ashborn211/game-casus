using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : InventorySlot
{
    public bool isSelected = false;
    public GameObject hotbarFrame;

    public void SetActiveSlot(bool selected)
    {
        isSelected = selected;
        hotbarFrame.GetComponent<RawImage>().color = selected ? Color.yellow : Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetActiveSlot(isSelected);
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveSlot(isSelected);
    }
}
