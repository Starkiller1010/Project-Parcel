using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private int currentChoice = 0; // 0 for Resume, 1 for Quit
    public GameObject cursor;
    private bool isPaused = false;


    void Start()
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }
        else
        {
            Debug.LogError("Pause Menu GameObject not found in the scene.");
        }
        if (cursor == null)        
        {
            Debug.LogError("Cursor GameObject not found as a child of the PauseMenu.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                isPaused = !isPaused;
                pauseMenu.SetActive(isPaused);
                Time.timeScale = isPaused ? 0 : 1; // Pause or resume the game
            }
            else
            {
                Debug.LogError("Pause Menu GameObject not found in the scene.");
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentChoice = 1 - currentChoice; // Toggle between 0 and 1
            UpdateCursorPosition();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isPaused)
        {
            if (currentChoice == 0)
            {
                // Resume the game
                isPaused = false;
                if (pauseMenu != null)
                {
                    pauseMenu.SetActive(false);
                }
                Time.timeScale = 1; // Resume the game
            }
            else if (currentChoice == 1)
            {
                // Quit the game
                QuitGame();
            }
        }
    }
    
    private void UpdateCursorPosition()
    {
        if (cursor != null)
        {
            Vector3 newPosition = cursor.transform.localPosition;
            newPosition.y = currentChoice == 0 ? 0 : -200; // Adjust Y position based on choice
            cursor.transform.localPosition = newPosition;
        }
        else
        {
            Debug.LogError("Cursor GameObject not found in the scene.");
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1; // Ensure the game is not paused when the object is destroyed
    }

    private void QuitGame()
    {
        Application.Quit();
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}