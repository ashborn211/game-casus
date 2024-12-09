using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int baseHealth;  
    private GameObject objectGame;
    private GameObject parent;
    private GameObject grandParent;
    public bool death { get; private set; } = false;
    private bool isPlayer = false;
    private GameObject gameOverScreenObject;

    private GameOverScreen gameOverScreen;

    private MovementPlayer playerMovement;
    private HealthSlider healthSlider;
    private int _maxHealth;
    public int maxHealth{
        get{ return _maxHealth; }
        set{
            _maxHealth = value;
            healthSlider?.MaxSetHealth(_maxHealth);
        }
    }
    private int _health;
    private int health{
        get { return _health; }
        set 
        { 
            if(value > maxHealth){
                _health = maxHealth;
            }
            else{
                _health = value;
            }
            healthSlider?.SetHealth(_health);
        }
    }
    
    void Start()
    {
        objectGame = gameObject;
        parent = this.transform.parent.gameObject;
        if(parent.tag == "Player"){
            Debug.Log("player exists :3");
            isPlayer = true;
            healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<HealthSlider>();
            gameOverScreenObject = GameObject.FindWithTag("GameOverScreen");
            playerMovement = parent.GetComponent<MovementPlayer>();
            if(gameOverScreenObject != null){
                Debug.Log("gameOverScreenObject");
                gameOverScreen = gameOverScreenObject.GetComponent<GameOverScreen>();
            }
        }

        maxHealth = baseHealth;
        health = maxHealth;
    }
    void Update()
    {
        if(health <=0 && !death){
            Debug.Log(objectGame.name + " death ---------------------------------------------------------------------");
            death = true;
            gameOverScreen?.GameOver();
            parent.SetActive(false);
        }
    }
    public void ChangeHealth(int healthAmount){
        health += healthAmount;
    }

    public void SetHealthMaxHealth(){
        health = maxHealth;
    }

    public void Revive(){
        parent.SetActive(true);
        SetHealthMaxHealth();
        playerMovement?.Spawn();
        death = false;
    }

    public int CheckHealth(){
        return health;
    }

    public int CheckMaxHealth(){
        return maxHealth;
    }
}
