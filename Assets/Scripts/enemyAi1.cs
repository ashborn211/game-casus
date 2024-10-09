using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class enemyAi1 : MonoBehaviour
{
    public MovementPlayer movementPlayer;

    public float test2 = 0;
    public float movementSpeed = (float)6.0;

    private Vector3 velocity;
    
    private float velocityX;
    private float velocityZ; 

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.position.y <= 2.8)
        {
            movementSpeed = (float)4.5;
        }
        else
        {
            movementSpeed = 6;
        }
        test2 = Distance(transform.position.x, transform.position.z, movementPlayer.transform.position.x, movementPlayer.transform.position.z);
        if((test2 > 1.5) && (test2 < 20.0)){
            rb.velocity = Vel();
        }
        else{
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    // float Angle(float x1, float y1, float x2, float y2)
    // {
    //     float difrenceX;
    //     float difrenceY;
    //     float result;
    //     if (x1 > x2){
    //         difrenceX = x1 - x2;
    //     }
    //     else {
    //         difrenceX = x2 - x1;
    //     }
    //     //Debug.Log("x " + difrenceX);
    //     if (y1 > y2){
    //         difrenceY = y1 - y2;
    //     }
    //     else {
    //         difrenceY = y2 - y1;
    //     }
    //     //Debug.Log("y " + difrenceY);
    //     if(difrenceX != 0.0 && difrenceY != 0.0){
    //         if (difrenceX > 0){
    //             result = ((Mathf.Atan(difrenceY/difrenceX))*(180/Mathf.PI));
    //         }
    //         else{
    //             result = ((Mathf.Atan(difrenceY/difrenceX))*(180/Mathf.PI))+180;
    //         }
    //     }
    //     else {
    //         result = 90;
    //     }
    //     Debug.Log("r " + result + " x " + difrenceX + " y " + difrenceY + " ratio " + (difrenceX/difrenceY));
    //     return result;
        
    // }

    float Angle(float x1, float y1, float x2, float y2)
    {
        float adjacent;
        float opposite;
        float result;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        int i;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
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

    float Distance(float x1, float y1, float x2, float y2){
        float adjacent;
        float opposite;
        float result;
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
        result = MathF.Sqrt(adjacent*adjacent + opposite*opposite);
        // Debug.Log("r " + result + " a " + adjacent + " o " + opposite);
        return result;
    }

    Vector3 Vel(){
        float angel = Angle(transform.position.x, transform.position.z, movementPlayer.transform.position.x, movementPlayer.transform.position.z)-270;

        // if(angel > 90){
        //     angel
        // }
        // else if(angel > 180){
        //     angel
        // }
        // else if(angel > 270){
        //     angel
        // }
        // else{
        //     angel - 90
        // }

        
        velocityZ = Mathf.Cos(angel*(MathF.PI/180))*movementSpeed;
        velocityX = Mathf.Sin(angel*(MathF.PI/180))*movementSpeed;
        velocity = new Vector3(velocityX, rb.velocity.y, velocityZ);
        return velocity;
    }
}