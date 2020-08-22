using System;
using System.Collections;
using UnityEngine;

//reference for timer: https://www.youtube.com/watch?v=qc7J0iei3BU
public class TimeController : MonoBehaviour
{
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    private void Awake()
    {
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
    public float GetTimePlaying()
    {
        return elapsedTime;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}