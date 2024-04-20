using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class QuestionLoader : MonoBehaviour
{
    [System.Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }



    public List<Question> LoadQuestionsFromJson()
    {

        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "questions.json");
        string jsonData = File.ReadAllText(jsonFilePath);
        List<Question> questions = JsonUtility.FromJson<QuestionList>(jsonData).questions;
        return questions;

    }
}