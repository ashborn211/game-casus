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
    private GameOverScreen gameOverScreen;
    private EnemyDrop enemyDropScript;
    private MovementPlayer playerMovement;
    private HealthSlider healthSlider;

    private int _maxHealth;
    public int maxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            healthSlider?.MaxSetHealth(_maxHealth);
        }
    }

    private int _health;
    private int health
    {
        get { return _health; }
        set
        {
            if (value > maxHealth)
            {
                _health = maxHealth;
            }
            else
            {
                _health = value;
            }
            healthSlider?.SetHealth(_health);
        }
    }

    // Awake is called before the Start
    void Awake()
    {
        objectGame = gameObject;
        parent = this.transform.parent.gameObject;
        if (parent.tag == "Player")
        {
            Debug.Log("[" + this + "] player exists :3");
            try{
                healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<HealthSlider>();
            }
            catch{
                Debug.LogError("[" + this + "] healthSlider could not be set 3:");
            }
            playerMovement = parent.GetComponent<MovementPlayer>();
            try{
                gameOverScreen =  GameObject.FindWithTag("GameOverScreen").GetComponent<GameOverScreen>();
            }
            catch{
                Debug.LogError("[" + this + "] gameOverScreen could not be set 3:");
            }
                
        }
        else{
            enemyDropScript = parent.GetComponent<EnemyDrop>();

            if (enemyDropScript == null)
            {
                Debug.LogError("[" + this + "] EnemyDrop component not found on parent");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = baseHealth;
        SetHealthMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !death)
        {
            Debug.Log("[" + this + "] " + parent.tag + " death");
            if (parent.tag != "Player")
            {
                enemyDropScript.ItemDrop();
            }
            death = true;
            gameOverScreen?.GameOver();
            parent.SetActive(false);

        }
    }

    public void ChangeHealth(int healthAmount)
    {
        health += healthAmount;
    }

    public void SetHealthMaxHealth()
    {
        health = maxHealth;
    }

    public void Revive()
    {
        parent.SetActive(true);
        SetHealthMaxHealth();
        playerMovement?.Spawn();
        death = false;
    }

    public int CheckHealth()
    {
        return health;
    }

    public int CheckMaxHealth()
    {
        return maxHealth;
    }
}
