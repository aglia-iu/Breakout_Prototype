using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Shield : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseMovement = Input.mousePosition; // Collect the position of the mouse to control the shield.
        this.transform.rotation = Quaternion.Euler(0.0f, mouseMovement.y/2, 0.0f);

    }
}
