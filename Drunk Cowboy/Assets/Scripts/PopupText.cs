using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    TextMeshProUGUI textPro;
    List<string> showText = new List<string>();
    private int index;

    void Start()
    {
        textPro = GetComponent<TextMeshProUGUI>();
        showText.Add("Gun Slinger");
        showText.Add("Top Gun!");
        showText.Add("YeeHaw!");
        showText.Add("Bravo");
        showText.Add("Gun Master");
        showText.Add("Amazing");
        showText.Add("Fantastic");
        showText.Add("Marvelous");

        GenerateRandomWord();
    }

    public void GenerateRandomWord()
    {
        index = Random.Range(0, showText.Count);
    }
    private void Update()
    {        
        textPro.text = showText[index];    
    }
}
