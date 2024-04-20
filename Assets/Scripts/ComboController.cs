using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    // To access this script from anywhere in Game scene
    public static ComboController Instance;

    // Knowing if combo is active then increase it or else active it
    public bool isComboActive;
    // Combo Deactive time
    [Range(0f, 5f)] public float comboTime;
    // Combo counter x1, x2, x3 etc
    public int comboCounter;
    // Limit the combo
    public int maxCombo;
    // Display the comboCounter
    public Text txtCombo;
    // Animation of Combo Beat
    public Animator comboAnim;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // First combo will be deactive
        isComboActive = false;
        // And multiplyer will be only 1
        comboCounter = 1;
    }

    public int AddCombo(int score, int addedScore)
    {
        // Check if combo is activated or not
        if (isComboActive)
        {
            // Increase the counter
            comboCounter++;
            // Cnacle the previous iteration of StopCombo method
            CancelInvoke("StopCombo");

            // Recall call iteration of StopCombo method after a given time
						//example : if comboTime=3 then after 3 second Invoke "StopCombo" 
            Invoke("StopCombo", comboTime);
        }
        else
        {
            // OR else start the combo
            StartCombo();
        }

        // Play Animation clip from start
        comboAnim.Play("Combo Beat", 0, 0);

        // Clamp the value between 1 and max combo variable
        comboCounter = Mathf.Clamp(comboCounter, 1, maxCombo);

        // Display comboCounter value
        txtCombo.text = "x" + comboCounter;

        // Return the calculated score to display
        return score + (addedScore * comboCounter);
    }
    public void StartCombo()
    {
        // Active combo
        isComboActive = true;
        // If in combo time no kill then this method will invoke and deactivate combo
        Invoke("StopCombo", comboTime);
    }

    public void StopCombo()
    {
        // Deactivate combo
        isComboActive = false;
        // Counter Reset
        comboCounter = 1;
        // Display counter into text
        txtCombo.text = "x" + comboCounter;
    }
}
