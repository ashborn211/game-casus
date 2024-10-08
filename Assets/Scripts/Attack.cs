using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform mc;
    void Start()
    {
        mc = GetComponent<Transform>();
    }

    // Update is called once per frame
    
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
            
        // Debug.Log(mousePos.x + " x " + mousePos.y + " y of the " + Screen.width + " x " + Screen.height + " y");
        // Debug.Log(Angle(Screen.width/2, Screen.height/2,mousePos.x,mousePos.y));
        mc.localRotation = Quaternion.Euler(0,Angle(Screen.width/2, Screen.height/2,mousePos.x,mousePos.y)+135,0);

    
    }

        float Angle(float x1, float y1, float x2, float y2)
    {
        float adjacent;
        float opposite;
        float result;
        int i;
        if (x1 > x2){
            adjacent = x1 - x2;
        }
        else {
            adjacent = x2 - x1;
        }

        if (y1 > y2){
            opposite = y1 - y2;
        }
        else {
            opposite = y2 - y1;
        }

        if  (x1 < x2){
            if(y1 < y2){
                result = ((Mathf.Atan(adjacent/opposite))*(180/Mathf.PI))+270;
                i=0;
            }
            else{
                result = ((Mathf.Atan(opposite/adjacent))*(180/Mathf.PI))+0;
                i=1;
            }
        }       
        else{
            if(y1 < y2){
                result = ((Mathf.Atan(opposite/adjacent))*(180/Mathf.PI))+180;
                i=2;
            }
            else{
                result = ((Mathf.Atan(adjacent/opposite))*(180/Mathf.PI))+90;
                i=3;
            }
        }
        //Debug.Log("r " + result + " a " + adjacent + " o " + opposite + " ratio " + (adjacent/opposite) + " case " + i);
        return result;
    }
}
