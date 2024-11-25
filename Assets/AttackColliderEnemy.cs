using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderEnemy : MonoBehaviour
{
    private MeshCollider boxCollider;
    private int attackDamage = 3;//make it so it is via insatiate 

    private float update = 0.0f;

    private bool attackOnDelay = false;

    private float attackLength = 1.125f;//make it so it is via insatiate 

    private float attackDelay = 2.25f;//make it so it is via insatiate 

    private EnemyAi1 enemyAi;
    private GameObject grandParent;

    public bool inAttack { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        grandParent = (this.transform.parent.gameObject).transform.parent.gameObject;
        enemyAi = grandParent.GetComponent<EnemyAi1>();
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
        Debug.Log(collider.tag + " ----------------------------------------------------------------------------------------------------------------------");
        if (collider.GetComponent<Health>() != null && !(update > attackLength))//the code !(update > attackLength) may create problems when attacking multiple targets. the code collider.GetComponent<Health>() != null may make freindly fire possible
        {
            Health health = collider.GetComponent<Health>();
            health.ChangeHealth(attackDamage * -1);
            Debug.Log("wow");
        }
    }
}
