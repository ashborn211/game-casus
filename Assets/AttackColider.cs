using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacColider : MonoBehaviour
{
    private int attackDamge = 5;
    public BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            boxCollider.enabled = true;
            Debug.Log("true");
        }
        else{
            boxCollider.enabled = false;
            Debug.Log("false");
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.GetComponent<Health>() != null){
            Health health = collider.GetComponent<Health>();
            health.ChangeHealth(attackDamge*-1);
        }
    }

    public void SetDamage(int damage){
        attackDamge = damage;
    }
    
}
