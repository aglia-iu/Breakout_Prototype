using UnityEngine;
using TMPro;

/*
 * This class controls the output of the score.
 * 
 * METHODS: 
 * Start()
 * Update()
 * Restart()
 */
public class CanvasScript : MonoBehaviour
{
    public GameObject player; // The object in the scene that stores the PlayerController script.
    public TMP_Text scoreText; // The TextMeshPro object upon which the score will be projected.

    private PlayerController playerScript; // The PlayerController script taken off of the player object.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtain the PlayerController functionality
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScript.GetScore().ToString(); // Set the score on the Canvas.
    }

    /*
     * Reset the score upon restarting the game.
     */
    public void Restart()
    {
        playerScript.SetScore(playerScript.GetScore() * -1);
    }
}
