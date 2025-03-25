using UnityEngine;

/*
 * This class controls the movement of the ball with respect to the colliders that it interacts with. 
 * While the object itself has a rigidyBody component attached, the object does not utilize the rigidbody component at any point 
 * in the functionality. 
 * 
 * METHODS: 
 * NormalizeTransform(Vector3 transform)
 * ActivateForce(bool hitOne, bool hitTwo, bool hitThree, bool hitFour)
 * LinearInterpolation(float start, float end, float val)
 * Reflection(Vector3 transformNormalized, Collider other)
 * 
 */
public class CustomPhysics : MonoBehaviour
{
    public float speed; // The speed of the object
    public float linearDamping; // The linear damping applied to the object to slow it down.
    public float bounceFactor; // The bounce factor applied to the object.

    /* The NormalizeTranform function takes in a transform vector and reduces the magnitude to 1, while the object moves in the same direction. 
     * VARIABLES: Vector3 transform - the transform to be normalized.
     * 
     * RETURNS: A Vector3 transform with a magnitude of 1.
     */
    public Vector3 NormalizeTransform(Vector3 transform)
    {
        float magnitude = Mathf.Sqrt((transform.x * transform.x) + (transform.y * transform.y) + (transform.z * transform.z));

        return (transform / magnitude);
    }

    /*
     * The ActivateForce method takes in four boolean values representing the four surfaces for the physics object to hit, and returns a value of 
     * the speed, along with the direction, to be applied.
     * 
     * VARIABLES: bool hitOne - The first surface that can be hit by the physics object. 
     *            bool hitTwo - The second surface that can be hit by the physics object. 
     *            bool hitThree - The third surface that can be hit by the physics object. 
     *            bool hitFour - The fourth surface that can be hit by the physics object. 
     * RETURNS: 
     *            a float value indicating the speed to be applied to the object after colliding with these objects.
     */
    public float ActivateForce(bool hitOne, bool hitTwo, bool hitThree, bool hitFour)
    {
        if (hitOne || hitTwo || hitThree || hitFour)
        {
            return speed;
        }
        return -1.0f * speed;
    }
    /*
     * This function linearly interpolates between two objects from a starting to an ending point based on a set amount of time (also known as 
     * val.)
     * 
     * VARIABLES: 
     *          float start - The starting point of the linear interpolation.
     *          float end - The ending point of the linear interpolation.
     *          float val - The time required for the linear interpolation.
     *          
     * RETURNS: a float that is representative of the linear interpolation.
     */
    public float LinearInterpolation(float start, float end, float val)
    {
        return ((end - start) * val) + start;
    }

    /*
     * The Reflection function reflects a transform along the forward vector of a collider.
     * 
     * VARIABLES: 
     *          Vector3 transformNormalized - the normalized transform along which the object is reflected.
     *          Collider other - the collider along which the transform is reflected off of it's axis.
     *          
     * RETURN: A Vector3 value representing the value reflected off of the wall. 
     */
    public Vector3 Reflection(Vector3 transformNormalized, Collider other)
    {
        return Vector3.Reflect(transformNormalized, other.transform.forward);
         
        
    }
}
