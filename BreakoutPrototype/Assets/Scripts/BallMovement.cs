using UnityEngine;

/*
 * With this class I want to control the movement of the ball with respect to the walls and the character's shield. 
 */
public class BallMovement : MonoBehaviour
{
    //PRIVATE VARIABLES
    private Rigidbody rigidBody;
    private bool shieldHit = false;
    private bool wallHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.Log("This component does not have an attached rigidbody.");
        }
        //Vector3 forceToAdd = new Vector3(0.0f, 0.0f, -1.0f);
        rigidBody.AddForce(ForceVector(-1.0f));

        shieldHit = false;
        wallHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        ActivateForce();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "shield")
        {
            shieldHit = true;
            wallHit = false;
        }

        if (collider.gameObject.tag == "yellowWall" || collider.gameObject.tag == "redWall")
        {
            shieldHit = false;
            wallHit = true;
        }

    }

    private void ActivateForce()
    {
        Debug.Log("Shield: " + shieldHit + ", Wall: " + wallHit);
        if (shieldHit)
        {
            rigidBody.AddForce(ForceVector(1.0f));
        }
        else if (wallHit)
        {
            rigidBody.AddForce(ForceVector(-1.0f));

        }
    }

    private Vector3 ForceVector(float zVal)
    {
        return new Vector3 (0.0f, 0.0f, zVal);
    }
}
