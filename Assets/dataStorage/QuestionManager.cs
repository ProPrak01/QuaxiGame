using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class QuestionManager : MonoBehaviour
{
    public List<Question> questions;
    public InputField[] questionInputFields;
    public InputField[] optionInputFields;
    public Dropdown[] correctAnswerDropdowns;
    public Text feedbackText;


    [System.Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }
    void Start()
    {
        PopulateDropdownOptions();
    }

    void PopulateDropdownOptions()
    {
        for (int i = 0; i < correctAnswerDropdowns.Length; i++)
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            for (int j = 1; j <= 4; j++)
            {
                options.Add(new Dropdown.OptionData(j.ToString()));
            }
            correctAnswerDropdowns[i].ClearOptions();
            correctAnswerDropdowns[i].AddOptions(options);
        }
    }

    public void SaveQuestionsToJson()
    {
        // Clear existing questions
        questions.Clear();

        // Loop through each question input field
        for (int i = 0; i < questionInputFields.Length; i++)
        {
            string questionText = questionInputFields[i].text;
            if (string.IsNullOrEmpty(questionText))
                continue;

            // Create a new question
            Question newQuestion = new Question();
            newQuestion.questionText = questionText;

            // Set options for the question
            int startIndex = i * 4;
            newQuestion.options = new string[4];
            for (int j = 0; j < 4; j++)
            {
                newQuestion.options[j] = optionInputFields[startIndex + j].text;
            }

            newQuestion.correctOptionIndex = correctAnswerDropdowns[i].value;

            // Add the new question to the list of questions
            questions.Add(newQuestion);
        }

        // Convert the list of questions to JSON and write it to a file
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "questions.json"); string jsonData = JsonUtility.ToJson(new QuestionList { questions = this.questions });
        File.WriteAllText(jsonFilePath, jsonData);

        feedbackText.text = "Questions saved successfully.";
    }
}
