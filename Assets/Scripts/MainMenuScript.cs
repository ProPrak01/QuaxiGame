using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button musicOnButton;
    public Button musicOffButton;
    public Button soundOnButton;
    public Button soundOffButton;
    public Button fastButton;
    public Button goodButton;

    //A Global Reference variable of the camera which is used to show Bloom Effect in Scene 
    public GameObject globalPostProcessing;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("MusicOnOff") == 1)
        {
            musicOnButton.interactable = false;
            musicOffButton.interactable = true;
        }
        else
        {
            musicOnButton.interactable = true;
            musicOffButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("SoundOnOff") == 1)
        {
            soundOnButton.interactable = false;
            soundOffButton.interactable = true;
        }
        else
        {
            soundOnButton.interactable = true;
            soundOffButton.interactable = false;
        }

        //Enable or Disable All AudioSource as per Preference value 
        SoundManagerScript.instance.CheckAudio();

        if (PlayerPrefs.GetInt("BloomEffect") == 1)
        {
            fastButton.interactable = false;
            goodButton.interactable = true;
        }
        else
        {
            fastButton.interactable = true;
            goodButton.interactable = false;
        }
    }

    // Bloom Effect On Off
    public void BloomOnOff(int bloomId)
    {
        SoundManagerScript.instance.uiButtonClickSound.Play();   //Click Sound
        PlayerPrefs.SetInt("BloomEffect", bloomId);              // 1-true 0-false

        if (bloomId == 1)
        {
            globalPostProcessing.SetActive(true);
        }
        else
        {
            globalPostProcessing.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Music state selection
    public void MusicOnOff(int musicId)
    {
        SoundManagerScript.instance.uiButtonClickSound.Play();
        SoundManagerScript.instance.SetMusic(musicId);
    }

    //Sound state selection
    public void SoundOnOff(int soundId)
    {
        SoundManagerScript.instance.uiButtonClickSound.Play();
        SoundManagerScript.instance.SetSound(soundId);
    }

    //this function useful in calling next scene as per buildIndex
    public void NextSceneCall(int sceneIndex)
    {
            SceneManager.LoadScene(sceneIndex);
    }

    public void ExitCall()
    {
        Application.Quit();
    }
}
