using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualizer : MonoBehaviour
{
    public GameObject lineObject;
    public float multiplier;
    public float lineThickness = 0.05f;
    public void VisualizeLine(float velocityMagnitude)
    {

        lineObject.transform.localScale = new Vector3(lineThickness, lineThickness, velocityMagnitude * multiplier + lineThickness);
    }

}
