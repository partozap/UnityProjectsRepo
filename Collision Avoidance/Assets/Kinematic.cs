using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    //position comes from attached gameobject transform
    //Rotation as well. The text refers to this as orientation
    public Vector3 linearVelocity;
    public float angularVelocity; //this is in degrees. The text refers to this as rotation
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* Update is called once per frame*/
    void Update()
    { 
        ///These 3 lines are the global variables of the kinematic function
        ///Without them, the object won't move. 
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime);
        transform.eulerAngles += angularIncrement;

        /// This declares a local variable for us to manipulate in our other scripts.
        /// By doing this, we enable the use of velocities and
        /// update linear and angular velocities
        SteeringOutput steering = new SteeringOutput();

        ///This section of code enables the arrive function
        ///Note: when uncommenting the code, i discovered a quick shortcut
        ///hitting ctrl+u automatically makes all characters lowercase.
        Arrive myArrive = new Arrive();
        myArrive.Arriver = this;
        myArrive.target = target;
        steering = myArrive.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;

        ///This section of code enables the align function.


        /*AlignDemo myAlign = new AlignDemo();
        myAlign.character = this;
        myAlign.target = target;
        steering = myAlign.getSteering();
        if (steering != null)
        {
            linearVelocity += steering.linear * Time.deltaTime;
            angularVelocity += steering.angular * Time.deltaTime;
        }*/

        ///This section of code enables the face function
        ///Dev note: the object uses the clockwise rotation to math the angle rather than
        ///the closest direction. must be fixed

             /*  Face myFace = new Face();
                myFace.character = this;
                myFace.target = target;
                steering = myFace.getSteering();

                linearVelocity += steering.linear * Time.deltaTime;
                angularVelocity += steering.angular * Time.deltaTime;*/

        LookWYG myLookWYG = new LookWYG();
        myLookWYG.character = this;
        myLookWYG.target = target;
        steering = myLookWYG.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;
    }
}
