using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMash;            //TextMash Pro for timer.
    public bool timeRunning = false;            // to check the time counter.
    float timerDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        textMash = GetComponent<TextMeshProUGUI>();;
        timeRunning = true;
    }

    public void StartTimer()
    {
        if (timeRunning)
        { 
            if (timerDuration > 0)
            {               
                timerDuration -= Time.deltaTime;
                UpdateTimer(timerDuration);                              
            }
            else
            {
                timerDuration = 0;
                timeRunning = false;
            }
        }        
    }

    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        textMash.text = "Wait for Time " + Mathf.Ceil(timerDuration).ToString() + " s";
    }
}
