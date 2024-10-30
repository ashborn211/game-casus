using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    public MovementPlayer movementPlayer;
    private Transform mc;

    private float lastDirection = 0f;

    private Animator animator;

    private float inputDirX;
    private float inputDirZ;

    void Start()
    {
        mc = transform.parent;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        lastDirection = movementPlayer.lastDirection;
        mc.localRotation = Quaternion.Euler(0, lastDirection*-1, 0);

        inputDirX = Input.GetAxisRaw("Horizontal");
        inputDirZ = Input.GetAxisRaw("Vertical");

        if(inputDirX != 0 || inputDirZ != 0){
            animator.SetFloat("speed", 1);
        }
        else{
            animator.SetFloat("speed", 0);
        }
    }

    public void PlayAttackAnimation(){
        if (animator.GetBool("firstAttack")){
            animator.SetTrigger("attack");
            animator.SetBool("firstAttack", false);
        }
        if (!animator.GetBool("firstAttack")){
            animator.SetTrigger("attack");
            animator.SetBool("firstAttack", true);
        }
    }
}