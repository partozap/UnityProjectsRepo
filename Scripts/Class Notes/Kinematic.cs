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
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime);
        transform.eulerAngles += angularIncrement;

        /*update linear and angular velocities*/
        SteeringOutput steering = new SteeringOutput();


        Arrive myArrive = new Arrive();
        myArrive.Arriver = this;
        myArrive.target = target;
        steering = myArrive.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular* Time.deltaTime;

        //AlignDemo myAlign = new AlignDemo();
        //myAlign.character = this;
        //myAlign.target = target;
        //steering = myAlign.getSteering();
        //if (steering != null)
        //{
        //    linearVelocity += steering.linear * Time.deltaTime;
        //    angularVelocity += steering.angular * Time.deltaTime;
        //}
    }
}
