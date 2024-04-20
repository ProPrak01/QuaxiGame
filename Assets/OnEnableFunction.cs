using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnEnableFunction : MonoBehaviour
{
    public Text questionText;
    public Text feedbackText;

    private void OnEnable()
    {
        questionText.text = "";

        feedbackText.text = "";
    }
}
