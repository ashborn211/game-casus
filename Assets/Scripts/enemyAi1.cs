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

    public float angel;

    public AttackColliderEnemy attackColliderEnemy;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        angel = Angle(transform.position.x, transform.position.z, movementPlayer.transform.position.x, movementPlayer.transform.position.z)-270;
        if (rb.position.y <= 2.8)
        {
            movementSpeed = 4.5f;
        }
        else
        {
            movementSpeed = 6.0f;
        }
        test2 = Distance(transform.position.x, transform.position.z, movementPlayer.transform.position.x, movementPlayer.transform.position.z);
        if((test2 > 1.5f) && (test2 < 20.0f) && !attackColliderEnemy.inAttack){
            rb.velocity = Vel();
        }
        else{
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    float Angle(float x1, float y1, float x2, float y2)
    {
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

        if  (x1 < x2){
            if(y1 < y2){
                result = ((Mathf.Atan(adjacent/opposite))*(180/Mathf.PI))+270;
            }
            else{
                result = ((Mathf.Atan(opposite/adjacent))*(180/Mathf.PI))+0;
            }
        }       
        else{
            if(y1 < y2){
                result = ((Mathf.Atan(opposite/adjacent))*(180/Mathf.PI))+180;
            }
            else{
                result = ((Mathf.Atan(adjacent/opposite))*(180/Mathf.PI))+90;
            }
        }
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
        return result;
    }

    Vector3 Vel(){
        velocityZ = Mathf.Cos(angel*(MathF.PI/180))*movementSpeed;
        velocityX = Mathf.Sin(angel*(MathF.PI/180))*movementSpeed;
        velocity = new Vector3(velocityX, rb.velocity.y, velocityZ);
        return velocity;
    }
}