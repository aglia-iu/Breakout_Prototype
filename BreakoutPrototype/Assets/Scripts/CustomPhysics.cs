using UnityEngine;

public class CustomPhysics : MonoBehaviour
{
    

    public float ActivateForce(float speed, bool surfaceOne, bool surfaceTwo)
    {
        if (surfaceOne)
        {
            //shieldTransform = shield.transform;
            return speed;
        }
        else if (surfaceTwo)
        {
            return -1.0f * speed;
            //return speed;

        }
        return 0;
    }
    public float LinearInterpolation(float start, float end, float val)
    {
        return ((end - start) * val) + start;
    }
    public void Reflection(Vector3 inTransformNormalized, Collider other, Vector3 outTransformNormalized)
    {
        outTransformNormalized = Vector3.Reflect(inTransformNormalized, other.transform.forward) * -1.0f;
        //float dotProduct = 2.0f * Vector3.Dot(shieldTransformNormalized, other.transform.forward);
        //wallTransformNormalized = (-1.0f * new Vector3(
        //    (dotProduct * shieldTransformNormalized.x) + other.transform.forward.x,
        //    (dotProduct * shieldTransformNormalized.y) + other.transform.forward.y,
        //    (dotProduct * shieldTransformNormalized.z) + other.transform.forward.z
        //    )).normalized;

    }
}
