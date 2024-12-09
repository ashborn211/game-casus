using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackColliderSword : MonoBehaviour
{
    public ModelMovement modelMovement;
    
    private MovementPlayer movementPlayer;
    private GameObject grandParent;

    private Attack attack;
    private GameObject Parent;

    private int attackDamage = 5;
    private MeshCollider boxCollider;
    private Transform transformer;

    public Mesh swordCollider0;

    public Mesh spearCollider0;

    private float weaponSize;

    private float update = 0.0f;

    private float update2 = 0.0f;

    private float margin = 0.5f;

    private bool attackOnDelay = false;

    private float attackDelay = 0.6f;
    private float attackLength = 0.2f;

    private MeshFilter meshFilter; //for debugging

    private bool rightSlash = true;
    public enum WeaponType
    {
        sword = 0,
        spear,
    } 

    private WeaponType weaponType = (WeaponType)(-1);
    // Start is called before the first frame update
    void Start()
    {
        Parent = this.transform.parent.gameObject;
        grandParent = Parent.transform.parent.gameObject;
        attack = Parent.GetComponent<Attack>();
        movementPlayer = grandParent.GetComponent<MovementPlayer>();
        boxCollider = GetComponent<MeshCollider>();
        transformer = transform.GetComponent<Transform>();
        meshFilter = GetComponent<MeshFilter>(); //for debugging
        SetWeaponType(WeaponType.sword);
        SetWeaponSize((float)0.75);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightSlash){
            update2+=Time.deltaTime;
            if(update2 > attackDelay+margin){
                rightSlash = true;
            }
        }

        if (Input.GetButtonDown("Fire1") && !attackOnDelay)
        {
            if(rightSlash){
                modelMovement.PlayAttackAnimationToLeft();
                rightSlash = false;
                update2 = 0.0f;
            }
            else{
                modelMovement.PlayAttackAnimationToRight();
                rightSlash = true;
            }
            Debug.Log("[" + this + "] " + modelMovement);
            attackOnDelay = true;
            boxCollider.enabled = true;
            update = 0;
            movementPlayer.inAttack = true;
            attack.inAttack = true;
        }
        else if(attackOnDelay){
            update+=Time.deltaTime;
            if (update > attackLength){
                boxCollider.enabled = false;
            }
            else{
                boxCollider.enabled = true;
            }

            if (update > attackDelay)
            {
                attackOnDelay = false;
                movementPlayer.inAttack = false;
                attack.inAttack = false;
            }
        }
        else
        {
            boxCollider.enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("[" + this + "] collided with " + collider.name + " " + collider.tag);
        if (collider.GetComponent<Health>() != null && !(update > attackLength))// the code && !(update > attackLength) may create problems when attacking multiple targets
        {
            Health health = collider.GetComponent<Health>();
            health.ChangeHealth(attackDamage * -1);
            Debug.Log("[" + this + "] successfully attacked " + collider.name + " " + collider.tag);
        }
    }

    public void SetDamage(int damage)
    {
        attackDamage = damage;
    }

    public void SetWeaponSize(float size)
    {
        if (weaponSize != size)
        {
            SetWeaponSizeForWeapon(size);
            weaponSize = size;
        }
    }

    public void SetWeaponType(WeaponType type)
    {
        if (weaponType != type)
        {
            if (type == WeaponType.sword)
            {
                boxCollider.sharedMesh = swordCollider0;
                meshFilter.mesh = swordCollider0; //for debugging
            }
            else if (type == WeaponType.spear)
            {
                boxCollider.sharedMesh = spearCollider0;
                meshFilter.mesh= spearCollider0; //for debugging
            }
            weaponType = type;
            SetWeaponSizeForWeapon(weaponSize);
            
        }
    }

    private void SetWeaponSizeForWeapon(float size){
        if (weaponType == WeaponType.sword)
        {
            transformer.localPosition = new Vector3(math.sqrt(2) * size, 0, math.sqrt(2) * size);
            transformer.localScale = new Vector3(100 * size, 100 * size, 20);
            transform.localEulerAngles = new Vector3(90, 180, 0);
        }
        else if (weaponType == WeaponType.spear)
        {

        }
    }
}
