using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public Transform startingPoint;
    public Transform endPoint;
    private void Update()
    {
        //Draw a line from the player to the goal
        lineRenderer.SetPosition(0, startingPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);       
    }
}
