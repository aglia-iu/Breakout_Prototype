using TMPro;
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
    public Material redMat;
    public Material yellowMat;
    public TMP_Text scoreText;
    //PRIVATE VARIABLES
    private bool shieldHit = false;
    private bool wallHit = false;
    private bool groundHit = false;
    private bool boundaryHit = false;
    private Transform shieldTransform;
    private Vector3 shieldTransformNormalized;
    private Transform wallTransform;
    private Quaternion wallTransformRotation;
    private Vector3 wallTransformNormalized;
    private Vector3 boundaryTransformNormalized;
    private float random;
    private PlayerController player;
    private CustomPhysics customPhysics;
    private Vector3 startPos;
    private Quaternion startRot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = shield.GetComponentInParent<PlayerController>();
        customPhysics = gameObject.GetComponent<CustomPhysics>();

        startPos = this.transform.position;
        startRot = this.transform.rotation;

        shieldTransform = shield.transform;
        shieldHit = false;
        wallHit = true;
        RandomizeColor();
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
            shieldTransform = other.transform;
            shieldTransformNormalized = shieldTransform.forward.normalized;
            //Debug.Log("Shield: " + shieldTransformNormalized);
            shieldHit = true;
            wallHit = false;
            boundaryHit = false;
        }

        if (other.gameObject.tag == "wall" )
        {
            wallTransform = other.transform;
            Color wallColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            Color ballColor = this.gameObject.GetComponent<MeshRenderer>().material.color;

            if (wallColor == ballColor)
            {
                player.SetScore(1);
            }
            else if(wallColor != ballColor)
            {
                player.SetScore(-1);
            }
            Reflection(shieldTransformNormalized,other);
            RandomizeColor();

            shieldHit = false;
            wallHit = true;
            boundaryHit = false;
        }
        if (other.gameObject.tag == "ground")
        {
            //shieldHit = false;
            groundHit = true;
            boundaryHit = false;
        }
        if (other.gameObject.tag == "boundary")
        {
            Reflection(wallTransformNormalized,other);
            //shieldHit = false;
            //groundHit = false;
            //wallHit = false;
            boundaryHit = true;
        }

    }

    private void BouncePhysics(float time)
    {
        Vector3 force = new Vector3(0, 0, ActivateForce());
        Vector3 linearDampingFactor = new Vector3(0, (customPhysics.LinearInterpolation(0.0f, bounceFactor, time) * linearDamping), 0);
        //Debug.Log(force);

        if (shieldHit && shieldTransform!=null)
        {
            force = ( shieldTransformNormalized * ActivateForce());
        }
        if (wallHit && wallTransform!=null)
        {
            force = (wallTransformNormalized * ActivateForce());
        }
        if (boundaryHit)
        {
            force = (wallTransformNormalized* -1.0f * ActivateForce());
        }
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

    private void Reflection(Vector3 transformNormalized, Collider other)
    {
        wallTransformNormalized = Vector3.Reflect(transformNormalized, other.transform.forward) * -1.0f;
    }
    private float ActivateForce()
    {
        if (shieldHit)
        {
            shieldTransform = shield.transform;
            return speed;
        }
        else if (wallHit)
        {
            return -1.0f * speed;
            //return speed;

        }
        return 0;
    }

    
    private void RandomizeColor()
    {
        random = Random.value;

        if(random <= 0.5f)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = redMat;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = yellowMat;

        }
    }

    public void Restart()
    {
        this.transform.position = startPos;
        this.transform.rotation = startRot;
        shieldTransform = shield.transform;

        shieldHit = false;
        wallHit = true;
    }

    
}
