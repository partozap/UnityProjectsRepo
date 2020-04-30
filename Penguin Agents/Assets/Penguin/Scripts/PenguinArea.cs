
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class PenguinArea : MonoBehaviour
{
    //The agent inside the area
    public PenguinAgent penguinAgent;

    //The baby penguin inside the are
    public GameObject penguinBaby;

    //The TextMeshPro text that shows the cumulative reward of the agent
    public TextMeshPro cumulativeRewardText;

    //Prefab of a live fish
    public Fish fishPrefab;

    private List<GameObject> fishList;

    //This resets the area, including the fish and penguin placement
    public void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlaceBaby();
        SpawnFish(4, Academy.Instance.FloatProperties.GetPropertyWithDefault("fish_speed", 05f));

    }

    //Removes a specific fish from the area when it is eaten
    public void RemoveSpecificFish( GameObject fishObject)
    {
        fishList.Remove(fishObject);
        Destroy(fishObject);
    
    }

    //The number of fish remaining
    public int FishRemaining
    {
        get { return fishList.Count; }
    }

    //Choose a random position on the X-Z plane within a partial donut shape

    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        float angle = minAngle;

        if (maxRadius > minRadius)
        {
            //Pick a random radius
            radius = UnityEngine.Random.Range(minAngle, maxAngle);
        }

        if (maxAngle > minAngle)
        {
            //Pick a random angle
            angle = UnityEngine.Random.Range(minAngle, maxAngle);
        }

        //center position + forward vector rotated around the y axis by "angle" degrees, multiplies by "radius"
        return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
    }
    //Removes all fish from the area
    private void RemoveAllFish()
    {
        if (fishList != null)
        {
            for (int i = 0; i < fishList.Count; i++)
            {
                if (fishList[i] != null)
                {
                    Destroy(fishList[i]);
                }
            }
        }
        fishList = new List<GameObject>();
    }
    //Places the penguin in the area
    private void PlacePenguin()
    {
        Rigidbody rigidbody = penguinAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 1f) + Vector3.up * .5f;
        penguinAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }
    //Place the baby in the area
    private void PlaceBaby()
    {
        Rigidbody rigidbody = penguinBaby.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        penguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 0f, 1f) + Vector3.up * .5f;
        penguinBaby.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    //Spawn some number of fish in the area and set their swim speed
    private void SpawnFish(int count, float fishSpeed)
    {
        for (int i = 0; i < count; i++)
        {
            //spawn and place the fish
            GameObject fishObject = Instantiate<GameObject>(fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            fishObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

            //set the fish's parent to this area's transform
            fishObject.transform.SetParent(transform);

            //Keep track of the fish
            fishList.Add(fishObject);

            //set the fish speed
            fishObject.GetComponent<Fish>().fishSpeed = fishSpeed;
        }
    }
    //Called when the game starts
    private void Start()
    {
        //On the first Heuristic Training, commenting this out so it doesn't destroy the pre-built model
        ResetArea();
    }

    private void Update()
    {
        cumulativeRewardText.text = penguinAgent.GetCumulativeReward().ToString("0.00");
    }
}
