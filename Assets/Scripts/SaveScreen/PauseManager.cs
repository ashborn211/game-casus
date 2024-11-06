using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI (Canvas)
    private bool isPaused = false;  // Flag to check if the game is paused



    void Start()
    {
        // Ensure the pause menu is hidden initially
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle pause when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Function to pause the game
    public void PauseGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);  // Show the pause menu
        }
        Time.timeScale = 0f;  // Stop the game time
        isPaused = true;
    }

    // Function to resume the game
    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);  // Hide the pause menu
        }
        Time.timeScale = 1f;  // Resume the game time
        isPaused = false;
    }
}
