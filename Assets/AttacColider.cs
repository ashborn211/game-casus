using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacColider : MonoBehaviour
{
    private int attackDamge = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
