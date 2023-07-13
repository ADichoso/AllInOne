using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimerController : MonoBehaviour
{
    #region Singleton
    public static TimerController sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of TimerController has been detected");
        }
    }
    #endregion

    public TextMeshProUGUI timeCounter;

    private TimeSpan timePlaying;
    private float elapsedTime;
    private bool isTimePlaying;

    public void BeginTimer()
    {
        isTimePlaying = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer(int index)
    {
        totalTimes[index] = elapsedTime;
        isTimePlaying = false;
    }

    IEnumerator UpdateTimer()
    {
        while (isTimePlaying)
        {
            elapsedTime += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timeString = "Time: " + timePlaying.ToString("mm':'ss'.'ff");

            timeCounter.text = timeString;

            yield return null;
        }
    }


    public float[] totalTimes;
}
