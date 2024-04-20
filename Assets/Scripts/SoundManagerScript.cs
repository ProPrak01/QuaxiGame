using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    public AudioSource bgMusic;
    public AudioSource uiButtonClickSound;
    public AudioSource playerHitSound;
    public AudioSource playerDiedSound;
    public AudioSource playerGrapplingSound;
    public AudioSource coinCollectSound;
    public AudioSource enemyBlastSound;

    //Awake Method
    void Awake()
    {
        MakeSingleton();
    }

    //This GameObject Never Destroy
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //Check it is first time gameplay or not
        if (!PlayerPrefs.HasKey("FirstTimeSoundCheck"))
        {
            PlayerPrefs.SetInt("MusicOnOff", 1);
            PlayerPrefs.SetInt("SoundOnOff", 1);
            CheckAudio();

            PlayerPrefs.SetInt("FirstTimeSoundCheck", 0);
        }

        if (GetMusic() == 1)
        {
            bgMusic.enabled = true;
        }
        else
        {
            bgMusic.enabled = false;
        }
    }

    public void SetMusic(int isOn)
    {
        PlayerPrefs.SetInt("MusicOnOff", isOn);
        CheckAudio();
    }
    public int GetMusic()
    {
        return PlayerPrefs.GetInt("MusicOnOff");
    }
    public void SetSound(int isOn)
    {
        PlayerPrefs.SetInt("SoundOnOff", isOn);
        CheckAudio();
    }
    public int GetSound()
    {
        return PlayerPrefs.GetInt("SoundOnOff");
    }

    public void CheckAudio()
    {
        if (GetSound() == 1)
        {
            uiButtonClickSound.enabled = true;
            playerHitSound.enabled = true;
            playerDiedSound.enabled = true;
            playerGrapplingSound.enabled = true;
            coinCollectSound.enabled = true;
            enemyBlastSound.enabled = true;
        }
        else
        {
            uiButtonClickSound.enabled = false;
            playerHitSound.enabled = false;
            playerDiedSound.enabled = false;
            playerGrapplingSound.enabled = false;
            coinCollectSound.enabled = false;
            enemyBlastSound.enabled = false;
        }

        if (GetMusic() == 1)
        {
            bgMusic.enabled = true;
        }
        else
        {
            bgMusic.enabled = false;
        }
    }
}
