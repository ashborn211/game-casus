using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider slider;

    // Awake is called before the Start
    void Awake (){
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MaxSetHealth(int value){
        slider.maxValue = value;
    }

    public void SetHealth(int value){
        slider.value = value;
    }
}
