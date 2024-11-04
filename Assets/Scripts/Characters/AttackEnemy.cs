using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public enemyAi1 enemyAi;

    public AttackColliderEnemy attackColliderEnemy;

    private Transform mc;
    void Start()
    {
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
