using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAndWeightDemo
{
    public SteeringBehaviour behaviour;
    public float weight;
}
public class BlendedSteeringDemo
{
    BehaviourAndWeightDemo[] behaviours;

    float maxAcceleration = 1f;
    float maxRotation = 5f;
    float weight = 0f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        foreach (BehaviourAndWeightDemo b in behaviours)
        {
            SteeringOutput s = b.behaviour.getSteering();
            if (s != null)
            {
                result.linear += s.linear * b.weight ;
                result.angular += s.angular * b.weight;
            }

        }

        //crop the result
        result.linear = result.linear.normalized * Mathf.Min(maxAcceleration, result.linear.magnitude);
        float angularAcc = Mathf.Abs(result.angular);
        if (angularAcc > maxRotation)
        {
            result.angular /= angularAcc;
            result.angular *= maxRotation;
        }

        return result;
    }

}
