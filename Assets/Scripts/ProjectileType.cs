using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{
    public string projectile_name;
    public float maxVelocity;
    public float buildUpSpeed;

    public Vector3 rotationSpeeds;

    public float groundDrag = 1;
    public float airDrag = 0;

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().drag = groundDrag;

        //Once the projectile enters a collision, tell the game controller that the projectile has stopped moving after a certain amount of time
        Invoke("nextStroke", 5f);
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().drag = airDrag;
        CancelInvoke();
        GameController.sharedInstance.hasWaitedTooLong = false;
    }

    public void nextStroke()
    {
        if (GameController.sharedInstance.playerController.followBall)
        {
            GameController.sharedInstance.hasWaitedTooLong = true;
        }
        else
        {
            GameController.sharedInstance.hasWaitedTooLong = false;
        }
    }
}
