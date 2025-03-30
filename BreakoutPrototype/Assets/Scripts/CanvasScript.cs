using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public GameObject player;
    public TMP_Text scoreText;

    private PlayerController playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScript.GetScore().ToString();
    }

    public void Restart()
    {
        playerScript.SetScore(playerScript.GetScore() * -1);
    }
}
