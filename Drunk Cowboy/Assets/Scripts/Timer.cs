using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textMash;            //TextMash Pro for timer.
    bool timeRunning = false;                   // to check the time counter.
    private float timerDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        textMash = GetComponent<TextMeshProUGUI>();
    }

    public void TimerToWait()
    {
        StartCoroutine(StartTimer());
    }
    IEnumerator StartTimer()
    {
        timeRunning = true;
        float timeLeft = timerDuration;
        while (timeLeft > 0)
        {
            textMash.text = "Wait for Time" + Mathf.Ceil(timeLeft).ToString() + "s";
            yield return new WaitForSeconds(0.1f);
            timeLeft -= 0.1f;
        }
    }
}
