using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class PenguinAgent : Agent
{
    //How fast the agent moves forward
    public float moveSpeed = 5f;

    //how fast the agent turns
    public float turnspeed = 180f;

    //Prefab of the heart that appears when the baby is fed
    public GameObject heartPrefab;

    //Prefab of the regurgitated fish that appears when the baby is fed
    public GameObject regurgitatedFishPrefab;

    //private variables that track everything
    private PenguinArea penguinArea;
    new private Rigidbody rigidbody;
    private GameObject baby;
    private bool isFull; //If true, penguin has a full stomach
    private float feedRadius = 0f;

    public override void Initialize()
    {
        base.Initialize();
        penguinArea = GetComponentInParent<PenguinArea>();
        baby = penguinArea.penguinBaby;
        rigidbody = GetComponent<Rigidbody>();

    }

    //Performs actions based on a vector of numbers
    public override void AgentAction(float[] vectorAction)
    {
        //convert the first action to forward movement
        float forwardAmount = vectorAction[0];

        //Convert the second action to turning left or right
        float turnAmount = 0f;
        if (vectorAction[1] == 1f)
        {
            turnAmount = -1f;
        }
        else if (vectorAction[1] == 2f)
        {
            turnAmount = 1f;
        }

        //Apply movement
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnspeed * Time.fixedDeltaTime);

        //Apply a tiny negative reward every step to encourage action
        if (maxStep > 0) AddReward(-1 / maxStep);
    
    }

    //Read inputs from the keyboard and convert them to a list of actions
    //Only called when the player wants to control the agent and as set behavior type to "Heuristic Only" in the Behavior Parameters inspector
    public override float[] Heuristic()
    {
        float forwardAction = 0f;
        float turnAction = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            forwardAction = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turnAction = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnAction = 2f;
        }

        return new float[] { forwardAction, turnAction };
    }

    //Reset the agent and area
    public override void AgentReset()
    {
        isFull = false;
        penguinArea.ResetArea();
        feedRadius = Academy.Instance.FloatProperties.GetPropertyWithDefault("feed_radius", 0f);
    }

    /// The code here is the corrected implementation off of the tutorial. The original tutorial had a patched out 
    /// syntax for everything. This is the corrected version. If you run into an error, this is where it may stem from

    public override void CollectObservations(MLAgents.Sensors.VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        
        //Whether the penguin has eaten a fish (1 float = 1 value)
        sensor.AddObservation(isFull);

        //Distance to the baby (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(baby.transform.position, transform.position));

        //Direction to baby (1 Vector3 = 3 values)
        sensor.AddObservation((baby.transform.position - transform.position).normalized);

        //Direction penguin is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward);
    }

    private void FixedUpdate()
    {
        /*Request a decision every 5 steps. RequestDecision() automatically calls RequestAction(),
         * but for the steps in between, we need to call it explicitly to take action using 
         * the results of the previous decision */

        //Use "StepCount" rather than "GetStepCount()"
        if (StepCount %5 == 0)
        {
            RequestDecision();
        }

        else
        {
            RequestAction();
        }

        // Test if the agent is close enough to feed the baby
        if (Vector3.Distance(transform.position, baby.transform.position) < feedRadius)
        {
            //Close enough, try to feed the baby
            RegurgitateFish();
        }

    }

    //When the agent collides with something, take action
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("fish"))
        {
            //Try to eat the fish
            EatFish(collision.gameObject);
        }
        else if (collision.transform.CompareTag("baby"))
        {
            //try to feed the baby
            RegurgitateFish();
        }
    }
    //Check if agent is full, if not, eat the fish and get a reward
    private void EatFish(GameObject fishObject)
    {
        if (isFull) return; //Can't eat on a full stomach
        isFull = true;

        penguinArea.RemoveSpecificFish(fishObject);

        AddReward(1f);
    }

    //Check if agent is full, if yes, feed the baby

    private void RegurgitateFish()
    {
        if (!isFull) return; //Nothing to regurgitate
        isFull = false;

        //Spawn regurgitated fish
        GameObject regurgitatedFish = Instantiate<GameObject>(regurgitatedFishPrefab);
        regurgitatedFish.transform.parent = transform.parent;
        regurgitatedFish.transform.position = baby.transform.position;
        Destroy(regurgitatedFish, 4f);

        //Spawn a heart
        GameObject heart = Instantiate<GameObject>(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.position = baby.transform.position + Vector3.up;
        Destroy(heart, 4f);

        AddReward(1f);

        if (penguinArea.FishRemaining <= 0)
        {
            Done();
        }
    }
    
}
