using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackColiderSpear : MonoBehaviour
{
    private int attackDamge = 5;
    private MeshCollider boxCollider;
    private Transform transformer;

    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<MeshCollider>();
        transformer = transform.GetComponent<Transform>();
        SetWeaponSize((float)1.0);
    }

    // Update is called once per frame
    // void Update()
    // {
        

    //     if(Input.GetButtonDown("Fire1")){
    //         boxCollider.enabled = true;
    //         Debug.Log("true");
    //     }
    //     else{
    //         boxCollider.enabled = false;
    //         Debug.Log("false");
    //     }
    // }

    // private void OnTriggerEnter(Collider collider){
    //     if(collider.GetComponent<Health>() != null){
    //         Health health = collider.GetComponent<Health>();
    //         health.ChangeHealth(attackDamge*-1);
    //     }
    // }

    public void SetDamage(int damage){
        attackDamge = damage;
    }
    
    public void SetWeaponSize(float size){
        transformer.localPosition = new Vector3(0, 0, size);
        transformer.localScale = new Vector3(size*(float)0.5, size, (float)0.75);
    }
}
