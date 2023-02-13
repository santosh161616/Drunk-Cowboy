using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    TextMeshProUGUI textPro;
    List<string> showText = new List<string>();
    private int index;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        textPro = GetComponent<TextMeshProUGUI>();
        showText.Add("Gun Slinger");
        showText.Add("Top Gun!");
        showText.Add("YeeHaw!");
        showText.Add("Bravo");
        showText.Add("Gun Master");
        showText.Add("Amazing");
        showText.Add("Fantastic");
        showText.Add("Marvelous");

    }

    public void GenerateRandomWord()
    {
        index = Random.Range(0, showText.Count);        
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        textPro.text = showText[index];
        anim.Play("pop");                   // Needs Review
        yield return new  WaitForSeconds(3f);
        textPro.text = "";             
    }
}
