using UnityEngine;

public class Seek : MonoBehaviour
{
    public Transform Target;
    public float MaxSpeed;


    // Update is called once per frame
    void Update()
    {
        //Establishes a new vector to fill data
        Vector3 v = new Vector3();
        //Takes the position of the block and the target, and subtracts the vectors in order to give a vector towards one
        v = Target.position - this.transform.position ;
        //Changes the value to a unit vector
        v = v.normalized;
        //Affects the speed of the motion
        v *= MaxSpeed *Time.deltaTime;
        //Updates the position
        this.transform.position += v;

        transform.LookAt(Target);
    }

   

}
