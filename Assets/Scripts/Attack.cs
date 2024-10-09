using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform mc;
    public bool inAttack = false;
    private float targetAngel = 0.0f;
    private float currentAngel = 0.0f;

    private float ratotasionSpeedTick = 0.0f;
    private bool clockwise = true;

    private float ratotasionSpeed = 360.0f; //per sec
    void Start()
    {
        mc = GetComponent<Transform>();
    }

    // Update is called once per frame

    void Update()
    {
        if (!inAttack)
        {
            Vector3 mousePos = Input.mousePosition;
            targetAngel = Angle(Screen.width / 2, Screen.height / 2, mousePos.x, mousePos.y) + 135;
            currentAngel = mc.eulerAngles.y;

            targetAngel = CorrectAngel(targetAngel);
            currentAngel = CorrectAngel(currentAngel);
            ratotasionSpeedTick = Time.deltaTime * ratotasionSpeed;
            if(CorrectAngel(targetAngel-currentAngel)>CorrectAngel(currentAngel-targetAngel)){
                currentAngel -= ratotasionSpeedTick;
                currentAngel = CorrectAngel(currentAngel);
                if(currentAngel > targetAngel || currentAngel+ratotasionSpeedTick < targetAngel){
                    mc.localRotation = Quaternion.Euler(0, currentAngel, 0);
                }
                Debug.Log("a");
            }
            else{
                currentAngel += ratotasionSpeedTick;
                currentAngel = CorrectAngel(currentAngel);
                if(currentAngel < targetAngel || currentAngel-ratotasionSpeedTick > targetAngel){
                    mc.localRotation = Quaternion.Euler(0, currentAngel, 0);
                }
                Debug.Log("b");
            }
        }
    }

    float Angle(float x1, float y1, float x2, float y2)
    {
        float adjacent;
        float opposite;
        float result;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        int i;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        if (x1 > x2)
        {
            adjacent = x1 - x2;
        }
        else
        {
            adjacent = x2 - x1;
        }

        if (y1 > y2)
        {
            opposite = y1 - y2;
        }
        else
        {
            opposite = y2 - y1;
        }

        if (x1 < x2)
        {
            if (y1 < y2)
            {
                result = ((Mathf.Atan(adjacent / opposite)) * (180 / Mathf.PI)) + 270;
                i = 0;
            }
            else
            {
                result = ((Mathf.Atan(opposite / adjacent)) * (180 / Mathf.PI)) + 0;
                i = 1;
            }
        }
        else
        {
            if (y1 < y2)
            {
                result = ((Mathf.Atan(opposite / adjacent)) * (180 / Mathf.PI)) + 180;
                i = 2;
            }
            else
            {
                result = ((Mathf.Atan(adjacent / opposite)) * (180 / Mathf.PI)) + 90;
                i = 3;
            }
        }
        //Debug.Log("r " + result + " a " + adjacent + " o " + opposite + " ratio " + (adjacent/opposite) + " case " + i);
        return result;
    }

    // bool ClockwiseFastestWay(float target, float current)
    // {
    //     float clockWay = target - current;
    //     float counterClockway = current - target;

    //     if (clockWay < 0.0f)
    //     {
    //         clockWay += 360.0f;
    //     }

    //     if (counterClockway < 0.0f)
    //     {
    //         counterClockway += 360.0f;
    //     }

    //     if (clockWay < counterClockway)
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    float CorrectAngel(float angel)
    {
        // while(angel >= 360.0f || angel < 0.0f){
        //     if(angel >= 360.0f){
        //         angel =- 360.0f;
        //     }
        //     else if(angel < 0.0f){
        //         angel =+ 360.0f;
        //     }
        // }
        angel -= (math.floor(angel/360.0f))*360.0f;
        return angel;
    }


}
