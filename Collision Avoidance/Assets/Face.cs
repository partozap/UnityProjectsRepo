using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : AlignDemo 
{
    /*public Kinematic character;
    public GameObject target;

    float maxAngularAcceleration = 10f; //4
    float maxRotation = 5f; //maxSpeed or max angular speed

    //the radius for arriving at the target
    float targetRadius = 1f;
    //radius for slowing down
    float slowRadius = 2f;
    //how long it takes to arrive to the target
    float timeToTarget = 0.1f;
    */
    protected override float GetTargetAngle()
    {
        Vector3 direction = target.transform.position - character.transform.position;
        ///In the text he uses an if statement to see if there's a value of zero,
        ///then calls a null. I can't seem to figure out how to implement this with
        ///the current syntax and C# ideas
        //if (direction.magnitude == 0)
        //{
        //    return null;
        //}

        float faceAngle = Mathf.Atan2(direction.x, direction.z);
        faceAngle *= Mathf.Rad2Deg;
        return faceAngle;
    }
    /*public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        float rotation = Mathf.DeltaAngle(character.transform.eulerAngles.y, GetTargetAngle());
        //float rotation = target.transform.eulerAngles.y - character.transform.eulerAngles.y;
        //rotation = mapToRange(rotation)
        //rotationSize = abs(rotation);
        float rotationSize = Mathf.Abs(rotation);
        if (rotationSize < targetRadius)
        {
            return null;
        }
        //if we are outside the slow radius then use max rotation
        float targetRotation = 0f; // target angular velocity
        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        //add direction back to our target angular velocity
        targetRotation *= rotation / rotationSize;

        //acceleration tries to get to the targetRotation
        result.angular = targetRotation - character.angularVelocity;
        result.angular /= timeToTarget;
        

        

        result.linear = Vector3.zero;


        return result;
    
   }*/
}
