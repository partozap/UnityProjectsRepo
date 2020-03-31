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
        result.angular = 0f;

        //get the direction to the target
        result.linear = target.transform.position - character.transform.position;

        ///This is where i try to figure out how to slow down my seek
        ///Here goes. 
        ///In the arrive function, we slow it down by creating a small condition 
        ///that alters the speed whenever it is within a certain range of an object. 
        ///If i want to adjust the speed over all, i should be able to assume that
        ///whatever settings in the single case would be viable if i want it to exist
        ///for all conditions.
        ///

        // give full accel
        return result;
    }
}
