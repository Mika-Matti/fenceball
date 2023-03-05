using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceChildScript : MonoBehaviour
{
    public GameObject fenceChild;
    private BoxCollider2D boxCollider;
    public int id;
    public bool expandHorizontally;
    public bool expanding;
    public Color active;
    public Color inactive;
    public float expandSpeed;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = fenceChild.GetComponent<BoxCollider2D>();
        expandSpeed = 30;
        expanding = true;
        fenceChild.GetComponent<SpriteRenderer>().color = active;
        AdjustSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (expanding)
        {
            Vector3 scale = fenceChild.transform.localScale;
            scale.x += expandSpeed / 10 * Time.deltaTime;
            fenceChild.transform.localScale = scale;
            AdjustSize();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Ball" && expanding)
        {
            Destroy(fenceChild);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.name.Contains("Fence") && other.GetComponent<FenceChildScript>().id == id)
        {
            return;
        }

        var tag = other.gameObject.tag;
        if (tag == "Obstacle" && expanding && fenceChild.transform.localScale.x < 2)
        {
            Destroy(fenceChild);
        }
        else if (tag == "Obstacle" && expanding && fenceChild.transform.localScale.x >= 2)
        {
            expanding = false;
            fenceChild.tag = tag;
            fenceChild.GetComponent<SpriteRenderer>().color = inactive;
            fenceChild.transform.localScale = new Vector2(Mathf.RoundToInt(fenceChild.transform.localScale.x), fenceChild.transform.localScale.y);

            var gameState = GameObject.Find("GameState").GetComponent<GameStateScript>();
            gameState.UpdateFencedArea(GetFenceArea());   
        } 
    }

    private float GetFenceArea()
    {
        Vector2 fenceDims = fenceChild.GetComponent<SpriteRenderer>().bounds.size;
        return fenceDims.x * fenceDims.y;
    }

    private void AdjustSize()
    {
        //Take the sizes of object and it's collider
        float fenceWidth = fenceChild.GetComponent<SpriteRenderer>().bounds.size.x;
        float xScale = fenceChild.transform.localScale.x;
        float realSize = fenceWidth * xScale;
        float boxWidth = 0.5f;
        float diff = 0.1f; // Fixed size difference we want between the two widths.
        float fixedRatio = ((realSize-diff)/realSize)*boxWidth;

        Vector3 newSize = boxCollider.size;
        newSize = new Vector3(fixedRatio, 0.9f, newSize.z);
        boxCollider.size = newSize;
    }
}