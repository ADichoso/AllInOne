using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public GameObject orientation;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    float yRotation, xRotation;
    public float rotate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime;

        //Rotate the camera around whenever yRotation has an input (to the left when A, to the right when D)
        yRotation += horizontalMovement * rotationSpeed;
        xRotation -= verticalMovement * rotationSpeed;

        //Clamp the xRotation
        xRotation = Mathf.Clamp(xRotation, -85, -10);

        //Orientation will store the x rotation in order to visualize a trajectory to the user.
        orientation.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //Rotate the whole player along the y axis
        transform.rotation = Quaternion.Euler(0, yRotation, 0);


        return xRotation;
    }

}
