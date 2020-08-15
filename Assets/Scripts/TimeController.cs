using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//reference for timer: https://www.youtube.com/watch?v=qc7J0iei3BU
public class TimeController : MonoBehaviour
{
    [SerializeField] private Text timeCounterText;
    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
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
    public string GetTimePlayingString()
    {
        return "Time: " + timePlaying.ToString("mm':'ss'.'ff");
    }
    public TimeSpan GetTimePlaying()
    {
        return timePlaying;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timeCounterText.text = GetTimePlayingString();
            yield return null;
        }
    }
}