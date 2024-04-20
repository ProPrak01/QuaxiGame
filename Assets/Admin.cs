using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Admin : MonoBehaviour
{
    public InputField password;
    public GameObject adminUI;
    public GameObject adminPanel;
    public GameObject scrollBar;
    public Text FeedBack;
    public void EnterAdmin()
    {
        FeedBack.text = "";

        if (password.text == "question1010")
        {
            scrollBar.SetActive(true);
            adminUI.SetActive(false);
            adminPanel.SetActive(true);

        }
        else
        {
            FeedBack.text = "Try Again";
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        adminUI.SetActive(true);
        adminPanel.SetActive(false);
        scrollBar.SetActive(false);

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }

}
