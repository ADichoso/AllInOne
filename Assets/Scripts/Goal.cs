using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        hasWon = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hasWon = false;
    }
}
