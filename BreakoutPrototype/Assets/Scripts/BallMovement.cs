using UnityEngine;
using UnityEngine.Rendering;

/*
 * With this class I want to control the movement of the ball with respect to the walls and the character's shield. 
 */
public class BallMovement : MonoBehaviour
{
    //PUBLIC VARIABLES
    public float speed;
    public float linearDamping;
    public float bounceFactor;
    public GameObject shield;
    //PRIVATE VARIABLES
    //private Rigidbody rigidBody;
    private bool shieldHit = false;
    private bool wallHit = false;
    private bool groundHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody>();
        //if (rigidBody == null)
        //{
        //    Debug.Log("This component does not have an attached rigidbody.");
        //}
        ////Vector3 forceToAdd = new Vector3(0.0f, 0.0f, -1.0f);
        //rigidBody.AddForce(ForceVector(-1.0f));
        
        shieldHit = false;
        wallHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hit the ground: " + groundHit + ", Hit the shield: " + shieldHit + ", Hit the Wall: " + wallHit);
        BouncePhysics(7.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "shield")
        {
            shieldHit = true;
            wallHit = false;
        }

        if (other.gameObject.tag == "wall" )
        {
            shieldHit = false;
            wallHit = true;
        }
        if (other.gameObject.tag == "ground")
        {
            groundHit = true;
        }
    }

    private void BouncePhysics(float time)
    {
        Vector3 force = new Vector3(0, 0, ActivateForce());
        Vector3 linearDampingFactor = new Vector3(0, (LinearInterpolate(0.0f, time) * linearDamping), 0);
        //Debug.Log(force);
        
        // If the ball hasn't hit the ground yet, let it fall down from the ground
        if (!groundHit) 
        {
            this.transform.position += (linearDampingFactor) * -0.5f ;
            this.transform.position += force;
        }
        //But if it has, let it rise for a while before falling down
        else
        {
            if (this.transform.position.y <= time)
            {
                this.transform.position += (linearDampingFactor) * 0.5f;
                this.transform.position += force;
            }
            else
            {
                groundHit = !groundHit;
            }
        }
    }

    private float ActivateForce()
    {
        if (shieldHit)
        {
            Debug.Log("Shield" + shield.transform.rotation);
            //this.transform.rotation = Quaternion.Normalize(shield.transform.rotation);
            Debug.Log("Ball" + this.transform.rotation);
            return speed;
        }
        else if (wallHit)
        {
            return -1.0f * speed;
        }
        return 0;
    }

    private float LinearInterpolate(float startValue, float time)
    {
        return startValue + (bounceFactor - startValue) * time;
    }

    
}
