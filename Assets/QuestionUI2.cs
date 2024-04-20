using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class QuestionUI2 : MonoBehaviour
{
    public Text questionText;
    public Dropdown optionDropdown;
    public int StartquestionId;
    public Text feedbackText;

    private List<string> optionsList; // List to store options for dropdown
    private int correctOptionIndex;

    public List<Question> LoadQuestionsFromJson()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "questions.json"); string jsonData = File.ReadAllText(jsonFilePath);
        List<Question> questions = JsonUtility.FromJson<QuestionList>(jsonData).questions;
        return questions;
    }
    [System.Serializable]
    
    public class QuestionList
    {
        public List<Question> questions;
    }
    public void DisplayQuestion()
    {
       
        List<Question> dataWeHave = LoadQuestionsFromJson();

        Question currentQuestion = dataWeHave[StartquestionId];
        questionText.text = currentQuestion.questionText;



        SetupQuestion(currentQuestion.questionText, currentQuestion.options, currentQuestion.correctOptionIndex);
        StartquestionId++;

    }



    // Method to set up the UI with question and options
    public void SetupQuestion(string question, string[] options, int correctIndex)
    {
        questionText.text = question;

        // Clear existing options and add new options
        optionsList = new List<string>(options);
        optionDropdown.ClearOptions();
        optionDropdown.AddOptions(optionsList);

        correctOptionIndex = correctIndex;

        // Disable submit button by default
      
    }
    public void OnSubmitButtonClick()
    {
        int selectedOptionIndex = optionDropdown.value;
        if (selectedOptionIndex == correctOptionIndex)
        {
            Time.timeScale = 1f; // Unpause the game
            ShowFeedback("Correct!");
        }
        else
        {
            ShowFeedback("Wrong try again!");
        }
    }

    // Method to show feedback text
    public void ShowFeedback(string message)
    {
        feedbackText.text = message;
    }

    
}
