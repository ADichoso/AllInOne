using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of GameController has been detected");
        }
    }
    #endregion

    public GameObject Player;
    public PlayerController playerController;
    public LevelsSystem levelsSystem;

    public int strokes = 1;

    public bool hasStoppedMoving = false;
    public bool hasWaitedTooLong = false;

    public bool isOutOfBounds = false;

    public TextMeshProUGUI strokesText;

    int[] totalStrokes;

    private void Start()
    {
        ProjectilePool.sharedInstance.InitializeObjectPool(playerController.playerRotate.orientation.transform);

        levelsSystem.activateLevel(playerController.gameObject.transform);

        strokes = 1;
        strokesText.text = "Turn " + strokes.ToString();

        totalStrokes = new int[levelsSystem.levels.Length];
        TimerController.sharedInstance.totalTimes = new float[levelsSystem.levels.Length];
    }

    private void Update()
    {
        if (isOutOfBounds)
        {
            //Return the projectile and player back to the original location
            Debug.Log("WARNING! OUT OF BOUNDS!");

            //Add one to the strokes
            strokes++;
            strokesText.text = "Turn " + strokes.ToString();

            //Get the selected projectile
            GameObject projectile_object = ProjectilePool.sharedInstance.selectedProjectileType.gameObject;

            //If it is not active, reset the launcher
            playerController.ballLauncher.hasLaunched = false;

            //Make the camera stop following the ball and bring it back to the player
            playerController.stopFollowingBall();

            //Reset the projectile pool
            ProjectilePool.sharedInstance.selectProjectile(projectile_object.GetComponent<ProjectileType>());

            isOutOfBounds = false;
        }
    }


    public void checkProjectileMovement(GameObject projectile_object)
    {
        if (!levelsSystem.isPaused) {

            //Get the selected projectile

            //Check the rigidbody it is not active
            hasStoppedMoving = projectile_object.GetComponent<Rigidbody>().IsSleeping() && projectile_object.GetComponent<Rigidbody>().useGravity;

            if (hasStoppedMoving || hasWaitedTooLong)
            {
                Debug.Log("Projectile has stopped moving");

                //Add one to the strokes
                strokes++;
                strokesText.text = "Turn " + strokes.ToString();

                //If it is not active, reset the launcher
                playerController.ballLauncher.hasLaunched = false;

                //Move the player onto the location of the ball
                Player.transform.position = projectile_object.transform.position;

                //Make the camera stop following the ball and bring it back to the player
                playerController.stopFollowingBall();

                //Reset the projectile pool
                ProjectilePool.sharedInstance.selectProjectile(projectile_object.GetComponent<ProjectileType>());

                if (levelsSystem.goals[levelsSystem.currentIndex].hasWon)
                {
                    //Player has won the level, go to the next level
                    nextLevel();
                }

                hasStoppedMoving = false;
                hasWaitedTooLong = false;
            }
        }
    }

    public void nextLevel()
    {
        //Stop the timer first
        TimerController.sharedInstance.EndTimer(levelsSystem.currentIndex);

        //Check first the current index of the level
        totalStrokes[levelsSystem.currentIndex] = strokes;

        if (levelsSystem.currentIndex == levelsSystem.levels.Length - 1)
        {
            //If we are currently in the last level, show the endgame screen
            levelsSystem.showEndScreen(TimerController.sharedInstance.totalTimes, totalStrokes);
        }
        else
        {
            //Show the regular game screen
            levelsSystem.showScoreScreen(TimerController.sharedInstance.timeCounter.text, strokes);
        }
    }


}
