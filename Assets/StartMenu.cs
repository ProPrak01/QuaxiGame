using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject guide;
    private void Start()
    {
        guide.SetActive(false);
    }
    public void Play()
    {

        SceneManager.LoadScene(2);

    }
    public void Guide()
    {

        guide.SetActive(true);

    }
    public void Admin()
    {

        SceneManager.LoadScene(1);

    }
    public void GuideButton()
    {

        guide.SetActive(false);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
