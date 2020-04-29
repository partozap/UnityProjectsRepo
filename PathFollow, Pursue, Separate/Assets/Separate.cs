using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separate 
{
    public Kinematic character;
    public GameObject[] targets;

    float maxAcceleration = 1f;

    float threshold = 10f;

    /*The next line here is the main component of the inverse square law
     * Essentially, this shapes how rapidly the change in acceleration is,
     * depending on how close one object is to another*/
    float decayCoefficient = 1000f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        foreach (GameObject target in targets)
        {
            Vector3 direction = character.transform.position - target.transform.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);
                direction.Normalize();
                result.linear += strength * direction;
            }
        }

        return result;
    }

}
