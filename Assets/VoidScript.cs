using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VoidScript : MonoBehaviour
{
    public Text ui;
    private void Start()
    {
        ui.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.gameObject.SetActive(true);

            StartCoroutine(LoadAfterDied());
        }

    }

    IEnumerator LoadAfterDied()
    {
        ui.text = "OH NOO!! WE WILL RESTART";
        yield return new WaitForSeconds(2f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ui.gameObject.SetActive(false);
        SceneManager.LoadScene(currentSceneIndex);
    }

}
