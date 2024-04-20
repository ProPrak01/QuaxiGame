using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameSceneScript : MonoBehaviour
{
    public static GameSceneScript Instance;

    //A Global Reference variable of the camera which is use to show Bloom Effect in Scene 
    public GameObject globalPostProcessing;

    // use GameOverPanel to visible and hide 
    public GameObject gameOverPanel;

// all Texts that are used in GameOverPanel

    // Show Kill Count of Hook
    public Text killsText;
    // Show Distance From Starting Point
    public Text distanceText;
    // Show Total Score which is calculated score value for money
    public Text totalScoreText;
    // In game current score
    public Text inGameScoreText;
    // Game highest Score
    public Text inGameHighScoreText;
    // Show calculated moeny
    public Text currentMoneyText;
    // Show final money value
    public Text totalMoneyText;

    //Text  Counters

    // In Game Score value
    public int inGameScoreCounter;
    // In Game High Score value
    public int inGameHighScoreCounter;
    // Calculate the Score money
    public int totalScore;
    // Count the Kills
    public int killCounter;
    // Count the distance from player to platform
    public int distanceCounter;
    // Current Money Value
    public int currentMoneyCounter;
    // Calculated Total Money Value (Final Money Value after All Calculation)
    public int totalMoneyCounter;

    //================ Pop Up Text =============//
    // Here we assign PopUpText prefab and Change the properties of its components according to our need using InitPopUpText Method.
    public Transform refPopUpText;

    // Show Current Score value
    public Text currentScoreDisplayText;


    // Start is called before the first frame update
    void Start()
    {
        //Get Preference value of Bloom Id
        int bloomId = PlayerPrefs.GetInt("BloomEffect");

        if (bloomId == 1)
        {
            // if bloomId==1 then Bloom Effect is On
            globalPostProcessing.SetActive(true);
        }
        else
        {
            // if bloomId==0 then Bloom Effect is Off
            globalPostProcessing.SetActive(false);
        }

        //Time Set in Normal Mode
        Time.timeScale = 1;

        gameOverPanel.SetActive (false);

        // Check if HighScore is available or not.
        if (!PlayerPrefs.HasKey ("HighScore")) 
				{
            PlayerPrefs.SetInt ("HighScore", 0);
        }

        // Get Highscore value from preference
        inGameHighScoreCounter = PlayerPrefs.GetInt ("HighScore");

        //Reset Current Score and Other Values in Game
        inGameScoreCounter = 0;
        distanceCounter = 0;
        killCounter = 0;
        totalScore = 0;
        totalMoneyCounter = 0;
    }

    private void Awake () {
				//Initialize Instance
        Instance = this;
    }

    public void GameOver () {
        gameOverPanel.SetActive (true);

        // Calculate total score Money by Current Score / 100
        totalScore = inGameScoreCounter / 100;

        // Current Money have total score + total hook killed + distance between player starting position and final position
        currentMoneyCounter = totalScore + killCounter + distanceCounter;

        // Add current money to total money
        totalMoneyCounter = totalMoneyCounter + currentMoneyCounter;

        // Save total money
        PlayerPrefs.SetInt ("TotalMoney", totalMoneyCounter);

        //Set New Highscore
        if (inGameScoreCounter > inGameHighScoreCounter) {
            inGameHighScoreCounter = inGameScoreCounter;
            PlayerPrefs.SetInt ("HighScore", inGameHighScoreCounter);
        }

        //Show all values in Text Field of GameOverPanel
        inGameScoreText.text = inGameScoreCounter.ToString ();
        inGameHighScoreText.text = inGameHighScoreCounter.ToString ();
        killsText.text = killCounter.ToString ();
        distanceText.text = distanceCounter.ToString ();
        totalScoreText.text = totalScore.ToString ();
        currentMoneyText.text = currentMoneyCounter.ToString ();
        totalMoneyText.text = totalMoneyCounter.ToString ();

        //Time freeze
		Time.timeScale = 0;
    }

    //This Function is used to call Scene via buildIndec Number
    public void SceneCall(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public  void InitPopUpText (Vector3 initPos, int value, bool isScore = true) 
    {
        // Make new Gameobject by instantiate PopUpText prefab to position and set default rotation.
        //Prefab Position = Hook Position
        GameObject text = Instantiate (refPopUpText, initPos+new Vector3(0f,1f,0f), Quaternion.identity).gameObject;
    
		// Make an empty string variable that will use to store sign and value.
        string str = "";

        // Assing sign before string value
        if (isScore) 
		{
            // If value is score then '+' sign will add.
            str += "+";
        } else 
		{
            // If value is not score(but money) then '$' sign will add.
            str += "$";
        }

        // Add value which will change in string.
        str += value;

        // Change text of Text Mesh Component to our string.
        text.GetComponent<TextMesh> ().text = str;

        // Destroy text after 2 second.
        Destroy (text, 2f);
    }

    //This function is used to call add score
    public void AddScore (int Score) 
    {
//      totalScore += Score;
        totalScore=ComboController.Instance.AddCombo(Score,totalScore);
        totalScoreText.text = totalScore.ToString ();
        currentScoreDisplayText.text = totalScoreText.text;
    }
}