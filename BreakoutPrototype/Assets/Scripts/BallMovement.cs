using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/*
 * This class controls the movement of the ball with respect to the colliders that it interacts with. 
 * While the object itself has a rigidyBody component attached, the object does not utilize the rigidbody component at any point 
 * in the functionality. 
 * 
 * METHODS: 
 * 
 * Start()
 * FixedUpdate()
 * OnTriggerEnter(Collision collision)
 * BounceMovement (float time)
 * RandomizeColor()
 * Restart()
 * 
 */
public class BallMovement : MonoBehaviour
{
    // PUBLIC VARIABLES
    public GameObject shield; // The shield gameObject attached to the player, which determines the direction the ball faces. 
    public Material redMat; // The red material of the ball. This will be compared against a wall to see if the player earns a point.
    public Material yellowMat; // The yellow material of the ball. This will be compared against a wall to see if the player earns a point.
    public TMP_Text scoreText; // This is the text on the Canvas that updates upon every change in the score. 

    // PRIVATE VARIABLES
        // Booleans that indicate whether or not the ball interacted with them.
    private bool shieldHit = false; // Indicates if the ball hit the shield. 
    private bool wallHit = false; // Indicates if the ball hit a wall (these are the blocks that are either yellow or red.)
    private bool groundHit = false; // Indicates if the ball hit the ground.
    private bool boundaryHit = false; // Indicates if the ball hit a boundary (these are the blocks that are grey, intended to keep the ball from leaving the game space.).

    private Transform curTransform; // The current transform that the ball is referencing to determine which direction to orient itself in.
    private Vector3 curTransformNormalized; // The normalized vector of the current transform. When multiplied with the speed, it gives the character force. 
    private Vector3 prevTransformNormalized; // The normalized vector of the previous transform, which can be used to reflect a ball off of a surface.
    private float random; // The random seed to change the colors of the ball. 
    private Vector3 startPos = Vector3.zero; // The starting position of the ball.
    private Quaternion? startRot = null;// The starting rotation of the ball.

    private PlayerController player; // The PlayerController component that stores the score of the user. 
    private CustomPhysics customPhysics; // The custom physics object used to manipulate the ball in program space.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtaining the scripts attached to GameObjects in the scene, including the ball itself.
        player = shield.GetComponentInParent<PlayerController>();
        customPhysics = this.gameObject.GetComponent<CustomPhysics>();
        player.Score2 = 5;

        // Obtaining the starting poition and rotation.
        startPos = this.transform.position;
        startRot = this.transform.rotation;

        // Setting the current normalized transform so that the ball travels straight, as well as the color of the ball.
        curTransformNormalized = new Vector3(0.0f, 0.0f, -1.0f);
        RandomizeColor();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // Debug statement to see which variables are active at any point in time. 
        Debug.Log("Hit the ground: " + groundHit + ", Hit the shield: " + shieldHit + ", Hit the Wall: " + wallHit + ", Hit the Boundary: " + boundaryHit);
       
        BounceMovement(Time.deltaTime); // Controls the movement of the ball.
    }

    /* The OnTriggerEnter function is used to control the ball depending on what it interacts with in the environment. In the environment, the ball
     * interacts with four main types of objects: 
     * 
     * Shield: This object orients the ball around the environment.
     * Wall: Collisions with this object determine if the player earns a point or not. 
     * Ground: This object reflects the ball off of it's surface and onto the environment of the object. 
     * Boundary: This object reflects the ball off, and back towards the user. 
     * 
     * VARIABLES: Collider other - represents the object that the ball is colliding with. 
     */
    private void OnTriggerEnter(Collider other)
    {
        // If the ball hits the shield
        if (other.gameObject.tag == "shield")
        {
            // Get the shield's normalized direction and rotation
            curTransform = other.transform; // To comment out
            curTransformNormalized = curTransform.forward.normalized;
            this.transform.rotation = curTransform.rotation;

            // Set only shieldHit to true
            shieldHit = true;
            wallHit = false;
            boundaryHit = false;

            // Record the shield's normalized transform to reference later.
            prevTransformNormalized = curTransformNormalized;
        }
        //  If the ball hits the wall
        if (other.gameObject.tag == "wall" )
        {
            // Get the wall's normalized direction and rotation
            curTransform = other.transform;

            // This is the direction in which the ball must move to indicate that the ball has been reflected off the wall upon collision with the wall. 
            curTransformNormalized = customPhysics.Reflection(prevTransformNormalized, other);
            this.transform.rotation = Quaternion.LookRotation(curTransformNormalized, this.transform.up);

            // Get the colors of the ball and the wall
            Color wallColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            Color ballColor = this.gameObject.GetComponent<MeshRenderer>().material.color;

            // If the ball hits the wall of the same color, increment the score by one.
            if (wallColor == ballColor)
            {
                player.SetScore(1);
            }
            // Otherwise, decrement the score by one.
            else if (wallColor != ballColor)
            {
                player.SetScore(-1);
            }
            // Change the balls color randomly
            RandomizeColor();

            // Set only wallHit to true
            shieldHit = false;
            wallHit = true;
            boundaryHit = false;

            // Record the wall's normalized transform to reference later.
            prevTransformNormalized = curTransformNormalized;
        }
        //  If the ball hits the ground
        if (other.gameObject.tag == "ground")
        {
            // Get the ground's transform
            //curTransform = other.transform;

            // Set only groundHit to true
            groundHit = true;

            // Record the ground's normalized transform to reference later.
            //prevTransformNormalized = curTransformNormalized;
        }
        //  If the ball hits the boundary
        if (other.gameObject.tag == "boundary")
        {
            // If the ball collides with the wall behind the player, reset the ball to it's original position.
            if (other.gameObject.name == "WallSouth")
            {
                Restart();
                return;
            }

            // Get the wall's normalized direction and rotation
            curTransform = other.transform;
            curTransformNormalized = customPhysics.Reflection(prevTransformNormalized,other);
            this.transform.rotation = Quaternion.LookRotation(curTransformNormalized, this.transform.up);

            // Set only booundaryHit to true
            shieldHit = false;
            wallHit = false;
            boundaryHit = true;

            // Record the boundary's normalized transform to reference later.
            prevTransformNormalized = curTransformNormalized;
        }

    }
    /* This method controls the bouncing and the movement of the ball. 
     * 
     * VARIABLES: float time - the time within which the ball bounces down and returns to the up position, is relative to the bouncefactor.
     */
    private void BounceMovement(float time)
    {
        Vector3 force = new Vector3(0, 0, customPhysics.ActivateForce(shieldHit, wallHit, boundaryHit, groundHit)); // The force with which the
                                                                                                                    // ball is propelled forwards. 
        Vector3 bounceVal = new Vector3( // The factor by which the object bounces up and down.
            0, 
            (customPhysics.LinearInterpolation(0.0f, customPhysics.bounceFactor, time) * (customPhysics.linearDamping)) * customPhysics.GravityCalculation(), 
            0
            );

        force = (curTransformNormalized 
            * customPhysics.ActivateForce(shieldHit, wallHit, boundaryHit, groundHit))
            //* customPhysics.GravityCalculation()
            ; // The normalized transform is dictated by the curTransformNormalized (set in the OnTriggerEnter() function on this script.)
        //Debug.Log("On Bounce Movement Start: " + curTransformNormalized);

        // If the ball hasn't hit the ground yet, let it fall down from the ground
        if (!groundHit) 
        {
            //Debug.Log("Before Ground Hit: " + curTransformNormalized);
            this.transform.position += (bounceVal) * -0.5f ; // The negative value indicates a downwards movement
            this.transform.position += force;
        }
        //But if it has, let it rise for a while before falling down
        else
        {
            if (this.transform.position.y <= 7.0f)
            {
                this.transform.position += (bounceVal) * 0.5f; // The positive value indicates a upwards movement
                this.transform.position += force;
            }
            // Once the ball has risen for a sufficient amount of time, set groundHit to false so that it can go down.
            else
            {
                groundHit = !groundHit;
            }
            //Debug.Log("After Ground hit: " + curTransformNormalized);

        }
    }

    /*
     * The RandmizeColor() function randomizes the color of the ball based on a random values between 0.0 and 1. 0 inclusive. The two colors 
     * randomized between are red and yellow.
     */
    private void RandomizeColor()
    {
        random = Random.value; // Random value between 0.0 and 1.0 inclusive.

        if(random <= 0.5f)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = redMat;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = yellowMat;

        }
    }

    /*
     * The Restart function is called whenever the restart button is pressed, or when the ball hits the boundary behind the player. 
     */
    public void Restart()
    {
        // Setting the position and rotation of the ball

        if (startPos != Vector3.zero && startRot != null)
        {
            Debug.Log(startPos);
            this.transform.position = startPos;
            this.transform.rotation = startRot.Value;
        }
        else
        {
            this.transform.position = new Vector3(1.18f, 4.97f, 11.61f);
            this.transform.rotation = new Quaternion();
        }


        // Setting the original normalized transform of the ball
        curTransformNormalized = new Vector3(0.0f, 0.0f, -1.0f);

        // Resetting the collision variables.
        shieldHit = false;
        groundHit = false;
        wallHit = false;
        boundaryHit = false;
    }

    
}
