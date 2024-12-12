using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform mc;
    public bool inAttack {get; set;} = false;
    private float targetAngel = 0.0f;
    private float currentAngel = 0.0f;

    private float rotationSpeedTick = 0.0f;
    private float rotationSpeed = 360.0f; //per sec
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
            targetAngel = Angle(Screen.width / 2, Screen.height / 2, mousePos.x, mousePos.y) + 90;
            currentAngel = mc.eulerAngles.y;

            targetAngel = CorrectAngel(targetAngel);
            currentAngel = CorrectAngel(currentAngel);
            rotationSpeedTick = Time.deltaTime * rotationSpeed;
            if(CorrectAngel(targetAngel-currentAngel)>CorrectAngel(currentAngel-targetAngel)){
                currentAngel -= rotationSpeedTick;
                currentAngel = CorrectAngel(currentAngel);
                if(currentAngel > targetAngel || currentAngel+rotationSpeedTick < targetAngel){
                    mc.localRotation = Quaternion.Euler(0, currentAngel, 0);
                }
            }
            else{
                currentAngel += rotationSpeedTick;
                currentAngel = CorrectAngel(currentAngel);
                if(currentAngel < targetAngel || currentAngel-rotationSpeedTick > targetAngel){
                    mc.localRotation = Quaternion.Euler(0, currentAngel, 0);
                }
            }
        }
    }

    float Angle(float x1, float y1, float x2, float y2)
    {
        float adjacent;
        float opposite;
        float result;
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
            }
            else
            {
                result = ((Mathf.Atan(opposite / adjacent)) * (180 / Mathf.PI)) + 0;
            }
        }
        else
        {
            if (y1 < y2)
            {
                result = ((Mathf.Atan(opposite / adjacent)) * (180 / Mathf.PI)) + 180;
            }
            else
            {
                result = ((Mathf.Atan(adjacent / opposite)) * (180 / Mathf.PI)) + 90;
            }
        }
        //Debug.Log("r " + result + " a " + adjacent + " o " + opposite + " ratio " + (adjacent/opposite) + " case " + i);
        return result;
    }

    float CorrectAngel(float angel)
    {
        angel -= (math.floor(angel/360.0f))*360.0f;
        return angel;
    }


}
