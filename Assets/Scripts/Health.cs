using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int baseHealth = 10;
    public int maxHealth;
    private int health;
    private GameObject objectGame;
    private GameObject parent;
    private GameObject grandParent;
    public bool death { get; private set; } = false;
    private bool isPlayer = false;

    private HealthSlider healthSlider;
    // public HealtText healtText;
    // public HealthSlider healthSlider
    
    void Start()
    {
        objectGame = gameObject;
        parent = this.transform.parent.gameObject;
        grandParent = this.transform.parent.gameObject;
        if(grandParent.tag == "Player"){
            Debug.Log("player exists :3");
            isPlayer = true;
            healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<HealthSlider>();
        }

        SetMaxHealth(baseHealth);
        SetHealth(maxHealth);
        // healtText?.SetHealthText(health, maxHealth);
        // healthSlider?.SetHealthSlider((float)health/(float)maxHealth*100f);
    }
    void Update()
    {
        if(health <=0 && !death){
            Debug.Log(objectGame.name + " death ---------------------------------------------------------------------");
            death = true;
            parent.SetActive(false);
        }
    }
    public void ChangeHealth(int healthAmount){
        SetHealth(health + healthAmount);
    }

    public void SetHealth(int healthAmount){
        health = healthAmount;
        if(health > maxHealth){
            health = maxHealth;
        }

        healthSlider?.SetHealth(health);
    }
    public void SetMaxHealth(int healthAmount){
        maxHealth = healthAmount;
        if(health > maxHealth){
            SetHealth(maxHealth);
        }

        healthSlider?.MaxSetHealth(healthAmount);
        // healtText?.SetHealthText(health, maxHealth);
        // healthSlider?.SetHealthSlider((float)health/(float)maxHealth*100f);
    }

    public void SetHealthMaxHealth(){
        health = maxHealth;
    }

    public void Revive(){
        parent.SetActive(true);
        death = false;
    }

    public int ChekHealth(){
        return health;
    }

    public int ChekMaxHealth(){
        return maxHealth;
    }
}
