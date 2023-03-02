using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject tile;
    public Color active;
    public Color passive;
    public Color solid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if(tile.tag!="Obstacle")
        {
            tile.GetComponent<SpriteRenderer>().color = active;
            tile.tag = "ActiveTile";
        }
    }

    private void OnMouseExit()
    {
        if(tile.tag!="Obstacle")
        {
            tile.GetComponent<SpriteRenderer>().color = passive;
            tile.tag = "Tile";
        }
    }
}
