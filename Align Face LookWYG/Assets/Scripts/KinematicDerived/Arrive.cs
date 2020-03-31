using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive
{
    public Kinematic Arriver;
    public GameObject target;

    public float maxSpeed = 2f; //maxSpeed or max angular speed
    float arriverSpeed; /*this will be what our desired speed at the time is. 
    We will determine the value of this by manipulating maxSpeed. In the text
    the author calls this target speed. Personally, that doesn't make sense
    since our target is a different object and this speed is concerning the 
    speed of whatever is arriving.*/

    //the radius for arriving at the target
    float targetRadius = 1f;
    //radius for slowing down
    float slowRadius = 10f;
    //how long it takes to arrive to the target
    public float timeToTarget = .5f;
    Vector3 direction;
    Vector3 arriverVelocity;

    /*This is where the steering is gained from getSteering*/

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        result.linear = new Vector3(0, 0, 1);
        /*result.angular = 0f; the text puts this at the end of the code.
        Since it's not used for anything and it's a zero value, we really
        don't need to place it either way. As long as it's set to 0, it only
        changes position in the code due to preference. And i prefer non used
        values at the top of the code.*/



        //get the direction to the target
        direction = target.transform.position - Arriver.transform.position;

        ///this provides the... well the distance. As stated in the name. 
        ///By making this a float, we are able to then compare this 
        ///value with our radii we set for states. 
        float distance = result.linear.magnitude;       

        //if the Arriver is at the target
        if (distance < targetRadius)
        {
            return null;
        }

        //if we're outside the Slow radius
        // ludicrous speed. Go
        // ok maybe not ludicrous since we have to scale it to the frame rate
        // so maybe just ridiculous speed.
        else if (distance > slowRadius)
        {

            arriverSpeed = maxSpeed * Time.deltaTime;
        }

        else
        ///if we're not outside the radius, and we're not at the target
        ///then we need to start pumping out some calculations to figure 
        ///out a measured deceleratioon.
        {
            
            arriverSpeed = maxSpeed * distance / slowRadius;
            //Great, but slowing down needs the knowledge of velocity
            //it's not just about speed.
            arriverVelocity = direction.normalized * arriverSpeed ;
            //in the text, this was 3 lines worth of code, but we can just simplify it here

            //now we plug in our new vector to our result return
            result.linear = arriverVelocity - Arriver.linearVelocity;
            //the above line updates the length to our target

            //the line below changes the length moved to reduce as it comes closer to the target
            result.linear /= timeToTarget;
        }

        return result;
    }




}
