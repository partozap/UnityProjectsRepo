using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextbookSeek
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration;

    virtual public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        result.linear = new Vector3(0, 0, 1);
        result.angular = 5f;

        //get the direction to the target
        result.linear = target.transform.position - character.transform.position;

        // give full accel
        return result;
    }
}
