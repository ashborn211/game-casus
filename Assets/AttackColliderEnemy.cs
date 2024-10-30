using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderEnemy : MonoBehaviour
{
    private MeshCollider boxCollider;
    private int attackDamage = 0;

    private float update = 0.0f;

    private bool attackOnDelay = false;

    private float attackLength = 1.125f;

    private float attackDelay = 2.25f;

    public EnemyAi1 enemyAi;

    public bool inAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackOnDelay){
            update+=Time.deltaTime;
            // Debug.Log(update);
            if (update > attackLength){
                boxCollider.enabled = false;
                inAttack = false;
            }
            else{
                boxCollider.enabled = true;
                inAttack = true;
            }

            if (update > attackDelay)
            {
                attackOnDelay = false;
            }
        }
        else if(enemyAi.test2 < 1.5)
        {
            attackOnDelay = true;
            boxCollider.enabled = true;
            update = 0;
        }
        else{
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name + " ----------------------------------------------------------------------------------------------------------------------");
        if (collider.GetComponent<Health>() != null && !(update > attackLength))//the code !(update > attackLength) may create problems when attacking multiple targets. the code collider.GetComponent<Health>() != null may make freindly fire possible
        {
            Health health = collider.GetComponent<Health>();
            health.ChangeHealth(attackDamage * -1);
            Debug.Log("wow");
        }
    }
}
