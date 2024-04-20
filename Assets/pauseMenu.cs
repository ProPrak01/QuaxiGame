using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionUI;
    private bool isPaused = false;
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (optionUI.activeSelf)
        {
           pauseMenuUI.SetActive(false);
           isPaused = false;

        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; // Resume game time
            isPaused = false;
        }
      
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void MainMenu()
    {
        
        Time.timeScale = 1f; // Resume game time

        SceneManager.LoadScene(0);
    }
}
