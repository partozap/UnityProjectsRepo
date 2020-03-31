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
    public float maxSpeed = 1f;
    public enum KinematicType { Arrive, Face, LookWYG, AlignDemo, FollowPathDemo, Pursue };
    public KinematicType myType;
    public GameObject[] myPath = new GameObject[4];
    FollowPathDemo myFollowPathDemo;
    // Start is called before the first frame update
    void Start()
    {
        myFollowPathDemo = new FollowPathDemo();
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

        switch (myType)
        {
            case KinematicType.Pursue:
                ///This section of code enables the PathFollow function
                ///
                Pursue myPursue = new Pursue();
                myPursue.character = this;
                myPursue.target = target;
                steering = myPursue.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;

                    ///The following condition helps to limit the speed of the object
                    if (linearVelocity.magnitude > maxSpeed)
                    {
                        linearVelocity = linearVelocity.normalized * maxSpeed;
                    }
                }
                break;
            case KinematicType.FollowPathDemo:
                ///This section of code enables the PathFollow function
                ///
                
                myFollowPathDemo.character = this;
                myFollowPathDemo.path = myPath;
                steering = myFollowPathDemo.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;
                    
                    ///The following condition helps to limit the speed of the object
                    if (linearVelocity.magnitude > maxSpeed)
                    {
                        linearVelocity = linearVelocity.normalized * maxSpeed;
                    }
                }
                break;
            
            case KinematicType.Arrive:
                ///This section of code enables the arrive function
                ///Note: when uncommenting the code, i discovered a quick shortcut
                ///hitting ctrl+u automatically makes all characters lowercase.
                Arrive myArrive = new Arrive();
                myArrive.Arriver = this;
                myArrive.target = target;
                steering = myArrive.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;
                }
                break;
            ///This section of code enables the align function.
            case KinematicType.AlignDemo:
                AlignDemo myAlign = new AlignDemo();
                myAlign.character = this;
                myAlign.target = target;
                steering = myAlign.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;
                }
                break;

            ///This section of code enables the face function

            case KinematicType.Face:
                Face myFace = new Face();
                myFace.character = this;
                myFace.target = target;
                steering = myFace.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;
                }
                break;

            case KinematicType.LookWYG:
                LookWYG myLookWYG = new LookWYG();
                myLookWYG.character = this;
                myLookWYG.target = target;
                steering = myLookWYG.getSteering();
                if (steering != null)
                {
                    linearVelocity += steering.linear * Time.deltaTime;
                    angularVelocity += steering.angular * Time.deltaTime;
                }
                Debug.Log(angularVelocity);
                break;
            
        }
    }
}
