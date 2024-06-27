using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movmentPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private float inputDirX;
    private float inputDirZ;
    public float moveSpeed = 7;
    public float camAngel = 45;
    private float lastDirection;
    private Vector3 velocity;

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
            moveSpeed = 3;
        }
        else
        {
            moveSpeed = 7;
        }

        inputDirX = Input.GetAxisRaw("Horizontal");
        inputDirZ = Input.GetAxisRaw("Vertical");
        if(inputDirX != 0 || inputDirZ != 0){
            if(inputDirZ >= 0){
                velocity = Velocity(moveSpeed, Direction(inputDirX*-1, inputDirZ) - camAngel + 90);
                lastDirection = Direction(inputDirX*-1, inputDirZ) - camAngel + 90;
            }
            else{
                velocity = Velocity(moveSpeed, Direction(inputDirX, inputDirZ*-1) - camAngel - 90);
                lastDirection = Direction(inputDirX*-1, inputDirZ);
            }
        }
        else{
            velocity = new Vector3(0, rb.velocity.y , 0);
        }

        rb.velocity = velocity;
    }

    public Vector3 Velocity(float speed,float angle){
        return new Vector3(speed*Mathf.Cos(angle/180*Mathf.PI), rb.velocity.y ,speed*Mathf.Sin(angle/180*Mathf.PI));
    }

    public float Direction(float fDirX,float fDirZ){

        return Mathf.Atan(fDirX/fDirZ)*180/Mathf.PI;
    }
}
