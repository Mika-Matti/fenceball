using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Object behavior upon collision with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        var closestPoint = other.ClosestPoint(transform.position);
        var xVel = rigidBody.velocity.x;
        var yVel = rigidBody.velocity.y;
        var range = 0.3;
        var tag = other.gameObject.tag;
        var nextPoint = new Vector2(transform.position.x+xVel,transform.position.y + yVel);

        if (tag == "Obstacle" || tag == "Ball" || tag == "Fence")
        {
            // If closest point is on same x-axis, then update yVelocity to change direction
            if (transform.position.x > closestPoint.x - range && 
                transform.position.x < closestPoint.x + range)
            {
                if(Mathf.Abs(closestPoint.y-nextPoint.y) < Mathf.Abs(transform.position.y-nextPoint.y))
                {
                    yVel = yVel * -1;
                }
            }
            // If closest point is on the same y-axis, then update xVelocity to change direction
            if(transform.position.y > closestPoint.y - range &&
                transform.position.y < closestPoint.y + range)
            {
                if (Mathf.Abs(closestPoint.x - nextPoint.x) < Mathf.Abs(transform.position.x - nextPoint.x))
                {
                    xVel = xVel * -1;
                }
            }
            setVelocity(xVel, yVel);
        } 
    }

    public void setVelocity(float xVelocity, float yVelocity)
    {
        rigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }
}
