using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackColiderSword : MonoBehaviour
{
    public MovementPlayer movementPlayer;
    public Attack attack;
    private int attackDamge = 5;
    private MeshCollider boxCollider;
    private Transform transformer;

    public Mesh swordColider0;

    public Mesh spearColider0;

    private float weaponSize;

    private float update = 0.0f;

    private bool attackOnDelay = false;

    private float attackDelay = 0.5f;
    private float attackLength = 0.2f;

    public enum WeaponType
    {
        sword = 0,
        spear,
    }

    private WeaponType weaponType = (WeaponType)(-1);
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<MeshCollider>();
        transformer = transform.GetComponent<Transform>();
        SetWeaponType(WeaponType.sword);
        SetWeaponSize((float)0.75);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attackOnDelay)
        {
            attackOnDelay = true;
            boxCollider.enabled = true;
            update = 0;
            movementPlayer.inAttack = true;
            attack.inAttack = true;
        }
        else if(attackOnDelay){
            update+=Time.deltaTime;
            // Debug.Log(update);
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
        Debug.Log(collider.name + " ----------------------------------------------------------------------------------------------------------------------");
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.ChangeHealth(attackDamge * -1);
        }
    }

    public void SetDamage(int damage)
    {
        attackDamge = damage;
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
                boxCollider.sharedMesh = swordColider0;
            }
            else if (type == WeaponType.spear)
            {
                boxCollider.sharedMesh = spearColider0;
            }
            weaponType = type;
            Debug.Log(weaponSize);
            SetWeaponSizeForWeapon(weaponSize);
            
        }
    }

    private void SetWeaponSizeForWeapon(float size){
        if (weaponType == WeaponType.sword)
        {
            transformer.localPosition = new Vector3(0, 0, math.sqrt(2) * size);
            transformer.localScale = new Vector3(100 * size, 100 * size, 20);
        }
        else if (weaponType == WeaponType.spear)
        {

        }
    }
}
