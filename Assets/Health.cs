using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int health;
    private GameObject objectGame;
    private bool death = false;
    // public HealtText healtText;
    // public HealthSlider healthSlider;
    

    
    void Start()
    {
        objectGame = gameObject;
        health = maxHealth;
        // healtText?.SetHealthText(health, maxHealth);
        // healthSlider?.SetHealthSlider((float)health/(float)maxHealth*100f);
    }
    void Update()
    {
        if(health <=0 && !death){
            Debug.Log(objectGame.name + " death");
            death = true;
            Destroy(gameObject);
        }
    }
    public void ChangeHealth(int healthAmount){
        health += healthAmount;
        if(health > maxHealth){
            health = maxHealth;
        }
        // healtText?.SetHealthText(health, maxHealth);
        // healthSlider?.SetHealthSlider((float)health/(float)maxHealth*100f);
    }
    public void ChangeMaxHealth(int healthAmount){
        maxHealth += healthAmount;
        if(health > maxHealth){
            health = maxHealth;
        }
        // healtText?.SetHealthText(health, maxHealth);
        // healthSlider?.SetHealthSlider((float)health/(float)maxHealth*100f);
    }

    public void SetHealthMaxHealth(){
        health = maxHealth;
    }

    public int ChekHealth(){
        return health;
    }

    public int ChekMaxHealth(){
        return maxHealth;
    }

    public bool ChekDeath(){
        return death;
    }

}
