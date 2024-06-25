using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    // Reference to the Canvas GameObject
    public GameObject canvas;

    // Key to toggle the Canvas (set to E key)
    private KeyCode toggleKey = KeyCode.E;

    // Update is called once per frame
    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            // Check if the canvas is assigned
            if (canvas != null)
            {
                // Toggle the active state of the Canvas
                canvas.SetActive(!canvas.activeSelf);
            }
            else
            {
                Debug.LogWarning("Canvas is not assigned in the ToggleCanvas script.");
            }
        }
    }
}
