using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Shield : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseMovement = Input.mousePosition; // Collect the position of the mouse to control the shield.
        //this.transform.rotation = Quaternion.Euler(0.0f, mouseMovement.y/4, 0.0f);
        this.transform.rotation = Quaternion.Euler(0.0f, LinearInterpolation(0.25f,- 0.1f, mouseMovement.y), 0.0f);
        // We also don't want the rotation going over 90 or under 90.
        //TODO: Add Lerp Function between -90 to 90.


    }

    private float LinearInterpolation(float start, float end, float val)
    {
        return ((end - start) * val) + start;
    }
}
