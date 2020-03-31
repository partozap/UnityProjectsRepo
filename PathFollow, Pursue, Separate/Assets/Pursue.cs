using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : TextbookSeek
{
    public float maxPrediction = .01f;
    public float prediction;
    public override SteeringOutput getSteering()

    {
        ///Step 1
        ///Calculate the target to delegate to seek
        ///work out the distance to the target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        float speed = character.linearVelocity.magnitude;

        if (speed <= distance/maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        character.transform.position += character.linearVelocity * prediction; 

        return base.getSteering();
    }
}
