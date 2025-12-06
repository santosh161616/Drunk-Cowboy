using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using cowboy.utils;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMash;            //TextMash Pro for timer.
    public bool timeRunning = false;            // to check the time counter.
    float timerDuration = 3f;
    Gun gunReference;

    // Start is called before the first frame update
    void Start()
    {
        textMash = GetComponent<TextMeshProUGUI>();;
        timeRunning = true;
        gunReference = FindObjectOfType<Gun>();
        GameEvents.Instance.OnTimerStart += StartTimer;
    }

    public void StartTimer(bool timeRunning)
    {
        this.timeRunning = timeRunning;
        if (timeRunning)
        {            
            if (timerDuration > 0)
            {              
                timerDuration -= Time.deltaTime;
                UpdateTimer(timerDuration);
                if(timerDuration <= 0)
                {
                    gunReference.DisableTimer();
                }
            }
            else
            {               
                GameEvents.Instance.TimerEnd();
                timeRunning = false;                
                timerDuration = 3f;
                gunReference.missedShots = 0;
            }
        }        
    }

    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        textMash.text = "You Missed!!!\n" + Mathf.Ceil(timerDuration).ToString() + " s";
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnTimerStart -= StartTimer;
    }
}
