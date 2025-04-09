using UnityEngine;

/*
 * This class is responsible for the control and the movement of the playable character. This character will move exclusively along the x-axis
 * and can be moved using the A (left) and D (right) keys.
 * 
 * METHODS: 
 * Start()
 * Update()
 * MovementPlayer()
 * GetScore()
 * SetScore(int increment)
 * Restart()
 */
public class PlayerController : MonoBehaviour
{
    //PUBLIC VARIABLES:
    public float speed = 0; // The speed of the character
    
    //PRIVATE VARIABLES:
    private Vector3 playerStartPos; // The start position of the player
    private Quaternion playerStartRot; // The start position of the player
    private float movementX; // The movement of the character along the X axis.
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStartPos = this.transform.position; // The starting position of the player.
        playerStartRot = this.transform.rotation; // The starting rotation of the player
        
    }

    // Update is called once per frame
    void Update()
    {
        MovementPlayer();
    }

    /*
     * This is the movement of the player along the X-Axis, and is called in Update()
     */
    public void MovementPlayer()
    {
        movementX = Input.GetAxis("Horizontal");
        Vector3 movementAlongX = new Vector3((movementX * speed)/2, 0, 0);

        if (this.transform.position.x >= 17.51f )
        {
            //Debug.Log("Player out of bounds");
            this.transform.position = new Vector3(17.5f, 2.52f, 0);
        }
        else if (this.transform.position.x <= -17.51f)
        {
            //Debug.Log("Player out of bounds");
            this.transform.position = new Vector3(-17.5f, 2.52f, 0);
        }
        else
        {
            this.transform.position += movementAlongX;
        }

    }

    /*
     * This is the GetScore() function, is a private Getter for the player's score.
     * 
     * RETURNS: An int representing the score. 
     */
    public int GetScore() 
    { 
        return score;
    }

    /*
     * This is the SetScore() function, is a private Setter for the player's score.
     * 
     * VARIABLES: An int representing the amount to increment the score by. 
     */

    public void SetScore(int increment)
    {
        score += increment;
    }

    /*
     * This is the Restart() function, is a private function used to Restart the player's score.
     */
    public void Restart()
    {
        this.transform.position = playerStartPos;
        this.transform.rotation = playerStartRot;
    }


}
