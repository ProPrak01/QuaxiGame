using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class QuestionUI : MonoBehaviour
{   
    public Text questionText;
    public InputField QNum;

    public Button[] optionButtons;

    private Question currentQuestion;

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
        int numericValue;
        string Qnum = QNum.text;
        int.TryParse(Qnum, out numericValue);

        List<Question> dataWeHave = LoadQuestionsFromJson();

        currentQuestion = dataWeHave[numericValue-1];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = currentQuestion.options[i];
        }
        

    }

    public void CheckAnswer(int selectedOptionIndex)
    {
        if (selectedOptionIndex == currentQuestion.correctOptionIndex)
        {
            Debug.Log("Correct Answer!");
        }
        else
        {
            Debug.Log("Wrong Answer!");
        }
    }
}