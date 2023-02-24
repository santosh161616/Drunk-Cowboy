using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    TextMeshProUGUI textPro;
    private readonly List<string> showText = new List<string>()
    {
        "Gun Slinger",
        "Top Gun!",
        "Yee Haw!",
        "Bravo",
        "Gun Master",
        "Amazing",
        "Fantastic",
        "Marvelous",
        "Giddyup",
        "Dagnabbit",
        "Git along, little dogie",
        "Whoopin' and a hollerin'",
        "Lasso",
        "Roundup",
        "Rawhide",
        "Highfalutin"
    };
    private int index;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        textPro = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayRandomText()
    {
        index = Random.Range(0, showText.Count);        
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        textPro.text = showText[index];
        anim.Play("pop", -1, 0f);                   
        yield return new  WaitForSeconds(1f);
        textPro.text = "";             
    }
}
