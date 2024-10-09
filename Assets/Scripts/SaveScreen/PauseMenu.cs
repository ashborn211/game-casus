using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject saveScreenCanvas;  // Reference to your save screen canvas GameObject
    private bool isPaused = false;

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC key pressed.");
            if (isPaused)
            {
                Debug.Log("Game is currently paused. Attempting to resume...");
                ResumeGame();  // If the game is already paused, resume it
            }
            else
            {
                Debug.Log("Game is currently running. Attempting to pause...");
                PauseGame();   // If the game is running, pause it and show the save screen
            }
        }
    }

    // Pause the game and show the save screen
    void PauseGame()
    {
        Debug.Log("Pausing game...");
        Time.timeScale = 0f;  // Freeze the game
        Debug.Log("Time.timeScale set to: " + Time.timeScale);

        saveScreenCanvas.SetActive(true);  // Show the save screen
        Debug.Log("Save screen activated.");

        isPaused = true;
        Debug.Log("Game is now paused.");

        // Unlock the cursor (if applicable)
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;
        Debug.Log("Cursor unlocked and made visible.");
    }

    // Resume the game and hide the save screen
    void ResumeGame()
    {
        Debug.Log("Resuming game...");
        Debug.Log("Time.timeScale before setting: " + Time.timeScale);

        Time.timeScale = 1f;  // Unfreeze the game by resetting the time scale
        Debug.Log("Time.timeScale set to: " + Time.timeScale);

        saveScreenCanvas.SetActive(false);  // Hide the save screen
        Debug.Log("Save screen deactivated.");

        isPaused = false;
        Debug.Log("Game is now running.");

        // Lock the cursor again (if applicable)
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;
        Debug.Log("Cursor locked and made invisible.");
    }
}
