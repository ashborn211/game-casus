using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameOverScreen : MonoBehaviour
{
    GameObject deathTextObject , respawnButtonObject , playerCapsule;
    TMP_Text deathText;
    Button respawnButton;
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        deathTextObject = GameObject.FindWithTag("DeathMessage");
        respawnButtonObject = GameObject.FindWithTag("RespawnButton");
        playerCapsule = GameObject.FindWithTag("PlayerCapsule");
        playerHealth = playerCapsule.GetComponent<Health>();
        deathText = deathTextObject.GetComponent<TMP_Text>();
        respawnButton = respawnButtonObject.GetComponent<Button>();
        respawnButton.onClick.AddListener(RespawnPlayer);
        deathText.text = "You skibidi beta have no rizz, we will send you back to ohio.";
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
