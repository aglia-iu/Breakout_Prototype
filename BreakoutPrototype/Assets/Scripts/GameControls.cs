using UnityEngine;

/// <summary>
/// GameControls is a script that oversees the controls of the entire project, and ensures that the programmer can comfortably make changes that impact the flow of the game, rather than the indiv
/// idual pieces involved in the creation of the game. 
/// </summary>
/// <METHODS>
/// BallInstantiate: A function that instantiates a new ball when the player acquires two more points in the game. 

public class GameControls : MonoBehaviour
{
    //PRIVATE VARIABLES
    private int recentScore;
    private GameObject score;
    private GameObject shield;
    private PlayerController playerControllerScript;

    //PUBLIC VARIABLES
    [SerializeField]
    public GameObject playerController;
    public GameObject ballPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recentScore = 0;
        if(playerController == null)
        {
            Debug.Log("PlayerController not acquired!");
        }
        if (ballPrefab == null)
        {
            Debug.Log("ballPrefab not acquired!");
        }
        playerControllerScript = playerController.GetComponent<PlayerController>();
        //shield = playerController.GetComponentInChildren<GameObject>();
        //score = GameObject.Find("Score");
        //if (shield == null)
        //{
        //    Debug.Log("Shield not acquired!");
        //}
        //if (score == null)
        //{
        //    Debug.Log("Score not acquired!");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateBall();
    }
    /// <summary>
    /// A function that instantiates a new ball when the player acquires two more points in the game.
    /// </summary>
    /// <VARIABLES>
    /// <OUTPUT> None
    private void InstantiateBall()
    {
        // Get the current score from PlayerController, and store it in a separate variable.
        int curScore = playerControllerScript.GetScore();
        // Check if current score is above recentScore, and if score is divisible by 2. 
        if((curScore > recentScore)&&(curScore % 2 == 0))
        {
            // Then instantiate a new instance of the ball. 
            //Instantiate<GameObject>(ballPrefab, InstantiateParameters (Shield = shield  ));
            GameObject ballInstantiate = Instantiate(ballPrefab);

            ballInstantiate.GetComponent<BallMovement>().Restart();

            // Lastly, will update recentScore (this only happens if the ball is instantiated)
            recentScore = curScore; 
        }
    }
}
