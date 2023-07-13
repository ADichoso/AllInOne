using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 movementLimits;

    public Vector3 movementSpeeds;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        //Move the obstacle
        moveObstacle();
    }

    void moveObstacle()
    {
        //Movement on each axis is independent of each other
        Vector3 newPosition = transform.position;
        //Keep moving the objects by the speed until it reaches the limit, then move backward onto the opposite direction until it reaches the negative limit



        //X Factor
        if (movementLimits.x != 0)
        {
            newPosition.x = Mathf.PingPong(Time.time * movementSpeeds.x, (movementLimits.x * 2)) + initialPosition.x - movementLimits.x;
        }


        //Y Factor
        if (movementLimits.y != 0)
        {
            if (transform.position.y <= initialPosition.y + movementLimits.y)
            {
                //Move to the positive direction
                newPosition.y += movementSpeeds.y * Time.deltaTime;
            }
            else if (transform.position.y >= initialPosition.y - movementLimits.y)
            {
                //Move to the negative direction
                newPosition.y -= movementSpeeds.y * Time.deltaTime;
            }
        }

        //Z Factor
        if (movementLimits.z != 0)
        {
            if (transform.position.z <= initialPosition.z + movementLimits.z)
            {
                //Move to the positive direction
                newPosition.z += movementSpeeds.x * Time.deltaTime;
            }
            else if (transform.position.z >= initialPosition.z - movementLimits.z)
            {
                //Move to the negative direction
                newPosition.z -= movementSpeeds.z * Time.deltaTime;
            }
        }

        //Once done, apply the new Position to the player

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.time);
    }
}
