using UnityEngine;

public class WallScript : MonoBehaviour
{
    // PRIVATE VARIABLES
    private float time = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 5.0f)
        {
            time = 0.0f;
        }
    }

    public void WallHide()
    {
        float curTime = time;
        if (time - curTime <= 5.0f)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        
    }



}
