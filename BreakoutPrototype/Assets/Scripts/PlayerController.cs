using UnityEngine;

/*
 * This class is responsible for the control and the movement of the playable character. This character will move exclusively along the x-axis
 * and can be moved using the A (left) and D (right) keys.
 */
public class PlayerController : MonoBehaviour
{
    //PUBLIC VARIABLES:
    public float speed = 0; // The speed of the character
    
    //PRIVATE VARIABLES:
    private Vector3 playerStartPos; // The start position of the player
    private Quaternion playerStartRot; // The start position of the player
    private float movementX; // The movement of the character along the X axis.
    private Rigidbody rigidBody; // The movement of the character along the X axis.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // The rigidbody of the character to add force to the character.
        playerStartPos = this.transform.position; // The starting position of the player.
        playerStartRot = this.transform.rotation; // The starting rotation of the player
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementPlayer();
        LockPlayerOnAxis();

    }

    public void MovementPlayer()
    {
        movementX = Input.GetAxis("Horizontal");
        Vector3 movementAlongX = new Vector3(movementX * speed, 0, 0);

        // Add the provided force to the rigidBody
        rigidBody.AddForce(movementAlongX);

    }

    public string LockPlayerOnAxis()
    {
        // Set the x and z component of the object to 0.0f.
        //this.rigidBody.;
        this.rigidBody.constraints = 
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezePositionZ | 
            RigidbodyConstraints.FreezeRotation;

        
        return "position and rotation set";
    }
}
