using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movmentPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private float inputDirX;
    private float inputDirZ;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
                rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputDirX = Input.GetAxis("Horizontal");
        inputDirZ = Input.GetAxis("Vertical");


        rb.velocity = new Vector3( inputDirX*moveSpeed, rb.velocity.y, inputDirZ*moveSpeed);
    }

    public float TrueDir(float dirA,float dirB){
        return (dirA+dirB)/2*Mathf.Cos(Mathf.Atan(dirA/dirB));
    }

    public float TrueDir2(float dirA,float dirB){
        if(dirA > dirB){
            return dirA*Mathf.Cos(Mathf.Atan(dirB/dirA)/*+cam.GetYRotation()*-1*/);
            
        }
        else{
            return dirB*Mathf.Cos(Mathf.Atan(dirB/dirA)/*+cam.GetYRotation()*-1*/);
        }
    }
}
