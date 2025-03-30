using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Shield : MonoBehaviour
{
    public GameObject physicsObj;
    private CustomPhysics customPhysics;

    
    private void Start()
    {
        customPhysics = physicsObj.GetComponent<CustomPhysics>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseMovement = Input.mousePosition; // Collect the position of the mouse to control the shield.
        this.transform.rotation = Quaternion.Euler(
            0.0f, 
            customPhysics.LinearInterpolation(-45f, 45f, mouseMovement.x/Screen.width), 
            0.0f
        );
        //Debug.Log(mouseMovement.x);
    }

    //private float LinearInterpolation(float start, float end, float val)
    //{
    //    return ((end - start) * val) + start;
    //}
}
