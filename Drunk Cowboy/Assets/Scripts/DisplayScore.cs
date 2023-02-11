using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    TextMeshProUGUI textPro;
    Gun gunReference;
    // Start is called before the first frame update
    void Start()
    {
        gunReference = FindObjectOfType<Gun>();
        textPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textPro.text = "Score: "+gunReference.GetScore().ToString();
    }
}
