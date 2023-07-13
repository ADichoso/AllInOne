using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelsSystem : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject[] spawnPoints;
    public Goal[] goals;

    public GameObject currentLevel;
    public int currentIndex = 0;

    public GameObject scorePanel;
    public GameObject endPanel;

    public bool isPaused = false;


    public void activateLevel(Transform player)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (currentIndex == i)
            {
                //Activate this one
                levels[i].SetActive(true);

                //Move the player to the spawnpoint
                player.position = spawnPoints[i].transform.position;
                player.localRotation = spawnPoints[i].transform.localRotation;
            }
            else
            {
                //deactivate all the others
                levels[i].SetActive(false);
            }
        }

        GameController.sharedInstance.strokes = 1;
        GameController.sharedInstance.strokesText.text = "Turn " + GameController.sharedInstance.strokes.ToString();

        //Begin the timer
        TimerController.sharedInstance.BeginTimer();
    }

    [Header("End Panel Objects")]
    public TextMeshProUGUI endPanelTime;
    public TextMeshProUGUI endPanelStrokes;

    public void showEndScreen(float[] times, int[] strokes)
    {
        isPaused = true;


        int totalStrokes = 0;
        float totalTime = 0;
        for (int i = 0; i < strokes.Length; i++)
        {
            totalStrokes += strokes[i];
            totalTime += times[i];
        }


        TimeSpan timePlaying;
        timePlaying = TimeSpan.FromSeconds(totalTime);

        string timeText = "Total Time: " + timePlaying.ToString("mm':'ss'.'ff");

        endPanel.SetActive(true);

        endPanelTime.text = timeText;
        endPanelStrokes.text = "Total Strokes: " + totalStrokes.ToString();
    }

    [Header("Score Panel Objects")]
    public TextMeshProUGUI scorePanelTime;
    public TextMeshProUGUI scorePanelStrokes;
    public TextMeshProUGUI scorePanelGoal;
    public TextMeshProUGUI scorePanelLevel1;
    public TextMeshProUGUI scorePanelLevel2;
    public void showScoreScreen(string timeText, int strokes)
    {
        isPaused = true;

        string goalType = strokesToGoalType(strokes);


        scorePanelTime.text = timeText;
        scorePanelStrokes.text = "Strokes: " + strokes.ToString();
        scorePanelGoal.text = goalType;
        scorePanelLevel1.text = "Hole" + (currentIndex + 1).ToString();
        scorePanelLevel2.text = "Hole" + (currentIndex + 1).ToString();

        scorePanel.SetActive(true);
    }

    string strokesToGoalType(int strokes)
    {
        /*
         Hole in one: 1
         Albatross: 2
         Eagle: 3
         Birdie: 4
         Par: 5
         Bogey: 7
         Double Bogey: 8
         Triple Bogey: 9
         Anything Else: strokes - 7 + "-over par"
         */

        switch (strokes)
        {
            case 1:
                return "Hole In One!";
            case 2:
                return "Albatross";
            case 3:
                return "Eagle!";
            case 4:
                return "Birdie!";
            case 5:
                return "Par!";
            case 6:
                return "Bogey!";
            case 7:
                return "Double Bogey!";
            case 8:
                return "Triple Bogey!";
            default:
                return (strokes - 7).ToString() + "-over par";
        }

    }

    public void onNextLevelButton(Transform player)
    {
        isPaused = false;

        GameController.sharedInstance.hasStoppedMoving = false;
        GameController.sharedInstance.hasWaitedTooLong = false;
        scorePanel.SetActive(false);

        //increment to the next level
        currentIndex++;

        activateLevel(player);
    }

    public void onRetryLevelButton(Transform player)
    {
        isPaused = false;

        GameController.sharedInstance.hasStoppedMoving = false;
        GameController.sharedInstance.hasWaitedTooLong = false;
        scorePanel.SetActive(false);

        activateLevel(player);
    }

    public void onMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
