using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWYG : AlignDemo

    /*When using this code in Unity, the target should be the object this script
     * is assigned to, since it gains the data from the movement of the object for
     * the direction it's going
     */
{
    //public Kinematic character;
    //public GameObject target;

    //float maxAngularAcceleration = 10f; //4
    //float maxRotation = 5f; //maxSpeed or max angular speed

    ////the radius for arriving at the target
    //float targetRadius = 1f;
    ////radius for slowing down
    //float slowRadius = 2f;
    ////how long it takes to arrive to the target
    //float timeToTarget = 0.1f;

    protected override float GetTargetAngle()
    {
        Vector3 velocity  = character.linearVelocity;
        float lookAngle = Mathf.Atan2(velocity.x,velocity.z);
        lookAngle *= Mathf.Rad2Deg;


        return lookAngle ;
    }
    //public SteeringOutput getSteering()
    //{
    //    SteeringOutput result = new SteeringOutput();

    //    float rotation = Mathf.DeltaAngle(character.transform.eulerAngles.y, GetTargetAngle());
    //    //float rotation = target.transform.eulerAngles.y - character.transform.eulerAngles.y;
    //    //rotation = mapToRange(rotation)
    //    //rotationSize = abs(rotation);
    //    float rotationSize = Mathf.Abs(rotation);
    //    if (rotationSize < targetRadius)
    //    {
    //        return null;
    //    }
    //    //if we are outside the slow radius then use max rotation
    //    float targetRotation = 0f; // target angular velocity
    //    if (rotationSize > slowRadius)
    //    {
    //        targetRotation = maxRotation;
    //    }
    //    else
    //    {
    //        targetRotation = maxRotation * rotationSize / slowRadius;
    //    }

    //    //add direction back to our target angular velocity
    //    targetRotation *= rotation / rotationSize;

    //    //acceleration tries to get to the targetRotation
    //    result.angular = targetRotation - character.angularVelocity;
    //    result.angular /= timeToTarget;
        

        

    //    result.linear = Vector3.zero;


    //    return result;
    //}
}
