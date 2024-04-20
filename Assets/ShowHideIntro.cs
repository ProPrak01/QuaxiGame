using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHideIntro : MonoBehaviour
{
    public Text ui;
    public string text;

    void Start()
    {

       
        StartCoroutine(MyCoroutine());
        
    }
    IEnumerator MyCoroutine()
    {
        ui.text = text;

        yield return new WaitForSeconds(2f);
        ui.text = "GO FORWARD->";
        yield return new WaitForSeconds(1.5f);

        ui.gameObject.SetActive(false);


    }


}
