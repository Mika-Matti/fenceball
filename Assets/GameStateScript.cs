using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateScript : MonoBehaviour
{
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        var ball1 = Instantiate(ball, new Vector2(0, 0), new Quaternion(0, 0, 0, 0));
        ball1.GetComponent<BallScript>().SetVelocity(5, 5);
        var ball2 = Instantiate(ball, new Vector2(2, 4), new Quaternion(0, 0, 0, 0));
        ball2.GetComponent<BallScript>().SetVelocity(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
