using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public bool hasLaunched;
    public GameObject orientation;

    public void launchBall(float velocity, GameObject selectedProjectile)
    {
        if (!hasLaunched)
        {
            selectedProjectile.GetComponent<Rigidbody>().AddForce(orientation.transform.forward * velocity, ForceMode.Impulse);
            selectedProjectile.GetComponent<Rigidbody>().AddTorque(selectedProjectile.GetComponent<ProjectileType>().rotationSpeeds, ForceMode.Impulse);
            hasLaunched = true;
        }
    }
}
