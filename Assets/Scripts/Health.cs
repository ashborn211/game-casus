using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    private int health;
    private GameObject objectGame;
    private GameObject parent;
    public bool death { get; private set; } = false;
    // public HealtText healtText;
    // public HealthSlider healthSlider
    
    void Start()
    {
        objectGame = gameObject;
        parent = this.transform.parent.gameObject;
        health = maxHealth;
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
