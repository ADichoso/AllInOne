using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameController.sharedInstance.isOutOfBounds = true;
    }
}
