using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PauseManager pauseManager;  // Reference to the PauseManager instance

    // Start method to find the PauseManager instance
    void Start()
    {
        // Find the PauseManager instance in the scene
        pauseManager = FindObjectOfType<PauseManager>();

        if (pauseManager == null)
        {
            Debug.LogError("PauseManager not found in the scene!");
        }
    }

    // Function to resume the game
    public void ResumeGame()
    {
        if (pauseManager != null)
        {
            pauseManager.ResumeGame();  // Call ResumeGame on the PauseManager instance
        }
    }

    // Function to exit the game and go to the Main Menu
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure game is unpaused
        SceneManager.LoadScene("Main-Menu");  // Load Main Menu scene
    }
}
