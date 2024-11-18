using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PauseManager pauseManager;

    void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();

        if (pauseManager != null)
        {
            Debug.Log("Found PauseManager is scene");

        }
        else
        {
            Debug.LogError("PauseManager not found in the scene!");
        }
    }

    public void ResumeButton()
    {

        pauseManager.ResumeGame();  

    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene("Main-Menu");  
    }
}
