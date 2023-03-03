using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FenceSpawnerScript : MonoBehaviour
{
    public GameObject fence;
    public GameObject tile;
    public TMP_Text directionIndicator;
    public Vector2 spawnPoint;
    private GameObject newFence;
    private bool expandHorizontally;
    private int counter;
    private Quaternion quaternion;

    // Start is called before the first frame update
    void Start()
    {
        expandHorizontally = false;
        quaternion = Quaternion.Euler(0, 0, 90);
        directionIndicator.text = "\u2195";
        counter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && NoActiveFences())
        {
            tile = GameObject.FindWithTag("ActiveTile");
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objectPos = GetNearestEdge(tile, mousePos);
            
            newFence = Instantiate(fence, objectPos, quaternion);
            var children = newFence.GetComponentsInChildren<FenceChildScript>();
            foreach (FenceChildScript child in children)
            {
                child.expandHorizontally = expandHorizontally;
                child.id = counter;
            }
            counter++;

            if (expandHorizontally)
            {
                expandHorizontally = false;
                quaternion = Quaternion.Euler(0, 0, 90);
                directionIndicator.text = "\u2195";
            } else
            {
                expandHorizontally = true;
                quaternion = Quaternion.Euler(0, 0, 0);
                directionIndicator.text = "\u2194";
            }
        } 
    }

    private bool NoActiveFences()
    {
        if (newFence != null)
        {
            var children = newFence.GetComponentsInChildren<FenceChildScript>();
            foreach (FenceChildScript child in children)
            {
                if (child.expanding)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private Vector2 GetNearestEdge(GameObject tile, Vector3 mousePos)
    {
        Vector2 closestEdge = tile.transform.position;
        float xLeft = tile.transform.position.x;
        float yTop = tile.transform.position.y;
        float width = tile.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = tile.GetComponent<SpriteRenderer>().bounds.size.y;
        float minDistance = Mathf.Infinity;
        List<Vector2> edges = new List<Vector2>();
        if(expandHorizontally) { 
            edges.Add(new Vector2(xLeft, yTop-height/2));
            edges.Add(new Vector2(xLeft+width, yTop-height/2));
        } else { 
            edges.Add(new Vector2(xLeft + width / 2, yTop));
            edges.Add(new Vector2(xLeft + width / 2, yTop - height));
        }

        foreach(Vector2 edge in edges)
        {
            float dist = Vector2.Distance(mousePos, edge);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestEdge = edge;
            }
        }

        return closestEdge;
    }
}
