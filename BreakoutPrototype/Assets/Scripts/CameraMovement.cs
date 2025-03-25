using UnityEngine;

/*
 * This class controls the movement of the camera with respect to the player in the scene
 * 
 * METHODS: 
 * Start()
 * Update()
 */
public class CameraMovement : MonoBehaviour
{
    // PUBLIC 
    [SerializeField]
    public GameObject player; // The player that the GameObject must follow. 
    //PRIVATE 
    private Vector3 startPos; // The current position of the camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = this.transform.position; // We first obtain how far away the camera is with regards to the character.
    }

    // Update is called once per frame
    void Update()
    {
        // Update the camera position based on the character's movements.
        this.transform.position = new Vector3(
            player.transform.position.x + startPos.x, 
            player.transform.position.y + startPos.y, 
            player.transform.position.z + startPos.z);
    }
}
