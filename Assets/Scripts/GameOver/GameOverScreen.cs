using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameOverScreen : MonoBehaviour
{
    GameObject respawnButtonObject , playerCapsule;
    Button respawnButton;
    Health playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnButtonObject = GameObject.FindWithTag("RespawnButton");
        playerCapsule = GameObject.FindWithTag("PlayerCapsule");
        playerHealth = playerCapsule.GetComponent<Health>();
        respawnButton = respawnButtonObject.GetComponent<Button>();
        respawnButton.onClick.AddListener(RespawnPlayer);
        Revive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver (){
        gameObject.SetActive(true);
    }

    public void Revive (){
        gameObject.SetActive(false);
    }

    public void RespawnPlayer(){
        playerHealth.Revive();
        Revive();
    }
}
