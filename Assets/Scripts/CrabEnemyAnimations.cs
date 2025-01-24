using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemyAnimations : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    private Rigidbody _rigidbody;

    private GameObject grandParent;
    private GameObject parent;
    void Start()
    {
        animator = GetComponent<Animator>();

        parent = this.transform.parent.gameObject;
        grandParent = parent.transform.parent.gameObject;

        _rigidbody = grandParent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.x >= 0.1 || _rigidbody.velocity.x <= -0.1 || _rigidbody.velocity.z >= 0.1 || _rigidbody.velocity.z <= -0.1)
        {
            animator.SetFloat("speed", 1);
            Debug.Log("1 x:" + _rigidbody.velocity.x + " z: " + _rigidbody.velocity.z);
        }
        else
        {
            animator.SetFloat("speed", 0);
            Debug.Log("0 x:" + _rigidbody.velocity.x + " z: " + _rigidbody.velocity.z);
        }
    }

    public void PlayAttackAnimationCrab()
    {
        animator.SetTrigger("Attack");
    }
}
