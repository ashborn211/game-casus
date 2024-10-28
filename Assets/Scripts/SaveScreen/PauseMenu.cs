using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject saveScreenCanvas; // Reference to your save screen canvas GameObject
    private bool isPaused = false;

    void Start()
    {
        // Ensure the save screen canvas is off by default
        if (saveScreenCanvas != null)
        {
            saveScreenCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC key pressed.");
            ToggleSaveScreen(); // Toggle save screen visibility
        }
    }

    // Toggle the save screen
    void ToggleSaveScreen()
    {
        isPaused = !isPaused; // Toggle the paused state
        if (saveScreenCanvas != null)
        {
            saveScreenCanvas.SetActive(isPaused); // Show or hide the save screen based on isPaused
        }
        Debug.Log(isPaused ? "Save screen activated." : "Save screen deactivated.");
    }
}
