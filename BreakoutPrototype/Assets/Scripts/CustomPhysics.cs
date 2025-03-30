using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    public float ActivateForce(float speed, bool hitOne, bool hitTwo, bool hitThree)
    {
        if (hitOne)
        {
            //shieldTransform = shield.transform;
            return speed;
        }
        else if (hitTwo)
        {
            //return -1.0f * speed;
            return speed;
        }
        else if (hitThree)
        {
            //return -1.0f * speed;
            return speed;

        }
        return -1.0f * speed;
    }
    public float LinearInterpolation(float start, float end, float val)
    {
        return ((end - start) * val) + start;
    }
    public Vector3 Reflection(Vector3 transformNormalized, Collider other)
    {
        return Vector3.Reflect(transformNormalized, other.transform.forward)
            //* -1.0f
            ;
        //float dotProduct = 2.0f * Vector3.Dot(shieldTransformNormalized, other.transform.forward);
        //wallTransformNormalized = (-1.0f * new Vector3(
        //    (dotProduct * shieldTransformNormalized.x) + other.transform.forward.x,
        //    (dotProduct * shieldTransformNormalized.y) + other.transform.forward.y,
        //    (dotProduct * shieldTransformNormalized.z) + other.transform.forward.z
        //    )).normalized;

    }
   
}
