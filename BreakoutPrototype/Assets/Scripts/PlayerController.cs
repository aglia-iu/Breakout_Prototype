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
    private Vector3 playerStart; // The start position of the player
    private float movementX; // The movement of the character along the X axis.
    private Rigidbody rigidBody; // The movement of the character along the X axis.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // The rigidbody of the character to add force to the character.
        playerStart = this.transform.position; // The starting position of the player.

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movementPlayer());

    }

    public string movementPlayer()
    {
        movementX = Input.GetAxis("Horizontal");
        Vector3 movementAlongX = new Vector3(movementX, 0, 0);

        // Add the provided force to the rigidBody
        Debug.Log(movementX);
        rigidBody.AddForce(movementAlongX);

        return ("Character Position is: "  + this.transform.position.ToString());
    }
}
