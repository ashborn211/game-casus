using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    GameObject deathTextObject;
    TMP_Text deathText;
    // Start is called before the first frame update
    void Start()
    {
        deathTextObject = GameObject.FindWithTag("DeathMessage");
        deathText = deathTextObject.GetComponent<TMP_Text>();
        deathText.text = "You skibidi beta have no rizz, we will send you back to ohio.";
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
}
