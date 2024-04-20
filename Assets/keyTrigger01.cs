using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyTrigger01 : MonoBehaviour
{
    public GameObject canvasObject; // Reference to the Canvas GameObject to turn on
    public GameObject ui2;
    private void Start()
    {
        ui2 = GameObject.FindGameObjectWithTag("Hook");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Turn on the Canvas GameObject
            canvasObject.SetActive(true);
            ui2.GetComponent<QuestionUI2>().DisplayQuestion();
            Time.timeScale = 0f; // Pause the game


            // Optionally, you can disable the key object after it's been collected
            gameObject.SetActive(false);
        }
    }
}
