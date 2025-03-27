using UnityEngine;

/*
 * With this class I want to control the movement of the ball with respect to the walls and the character's shield. 
 */
public class BallMovement : MonoBehaviour
{
    //PRIVATE VARIABLES
    private Rigidbody rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.Log("This component does not have an attached rigidbody.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forceToAdd = new Vector3(0.0f, 0.0f, -1.0f);

        rigidBody.AddForce (forceToAdd);
    }
}
