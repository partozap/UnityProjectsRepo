using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance
{
    public Kinematic character;
    float maxAcceleration = 1f;

    public Kinematic[] targets;

    float radius = 1f; ///our own collision radius
    public SteeringOutput getSteering()
    {
        //1. see if there's impending danger        
        float shortestTime = float.PositiveInfinity;

        Kinematic firstTarget = null;
        float firstDistance;
        Vector3 firstRelativePos;
        Vector3 firstRelativeVel;
        float firstMinSeparation;

        foreach (Kinematic target in targets)
        {
            Vector3 relativePos = target.transform.position - character.transform.position;
            Vector3 relativeVel = target.linearVelocity - character.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timetoCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            //is it close enough to care?

            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timetoCollision; 
            if (minSeparation > 2 * radius)
            {
                continue;
            }

            if ( timetoCollision > 0 && timetoCollision < shortestTime )
            {
                shortestTime = timetoCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        //2. if so do something about it
    }

}
