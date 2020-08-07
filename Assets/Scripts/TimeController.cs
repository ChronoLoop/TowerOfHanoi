using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//reference for timer: https://www.youtube.com/watch?v=qc7J0iei3BU
public class TimeController : MonoBehaviour
{
    public Text timeCounterText;
    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Start()
    {
        timeCounterText.text = "Time: 00:00.00";
        timerGoing = false;
    }
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounterText.text = timePlayingStr;
            yield return null;
        }
    }
}