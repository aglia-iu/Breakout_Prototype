using UnityEngine;

/*
 * This class is responsible for the control and the movement of the shield. The shield responds to movement along the x-axis by the player's 
 * mouse.
 * 
 * METHODS: 
 * Start()
 * Update()
 */
public class Shield : MonoBehaviour
{
    public GameObject physicsObj; // The object in the scene with a Physics component attached.
    private CustomPhysics customPhysics; // The customPhysics script taken off of the physicsObject.

    
    private void Start()
    {
        // Obtain the customPhysics functionality
        customPhysics = physicsObj.GetComponent<CustomPhysics>(); 

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseMovement = Input.mousePosition; // Collect the position of the mouse to control the shield.
        this.transform.rotation = Quaternion.Euler(
            0.0f,
            // Linearly interpolates the position of the shield relative to the position of the mouse on the screen.
            customPhysics.LinearInterpolation(-45f, 45f, mouseMovement.x/Screen.width), 
            0.0f
        );
    }

}
