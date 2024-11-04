using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyAi1 enemyAi;
    private GameObject parent;

    private AttackColliderEnemy attackColliderEnemy;
    private GameObject child;

    private Transform mc;
    void Start()
    {
        parent = this.transform.parent.gameObject;
        enemyAi = parent.GetComponent<EnemyAi1>();
        child = this.transform.Find("GameObject").gameObject;
        attackColliderEnemy = child.GetComponent<AttackColliderEnemy>();
        mc = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackColliderEnemy.inAttack){
            mc.localRotation = Quaternion.Euler(0, enemyAi.angel-90, 0);
        }
    }
}
