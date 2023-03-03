using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetterScript : MonoBehaviour
{
    public GameObject tile;
    public int mapWidth;
    public int mapHeight;
    public float area;
    public Vector2 objectPos;

    // Start is called before the first frame update
    void Start()
    {
        area = 0;
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                var newObject = Instantiate(tile, objectPos, new Quaternion(0, 0, 0, 0));
                var objectSprite = newObject.GetComponent<SpriteRenderer>();
                if(newObject.transform.position.x == -9 || newObject.transform.position.x == 8 ||
                    newObject.transform.position.y == 5 || newObject.transform.position.y == -4)
                {
                    newObject.tag = "Obstacle";
                    objectSprite.color = newObject.GetComponent<TileScript>().solid;
                }

                if(newObject.tag != "Obstacle")
                {
                    area += objectSprite.bounds.size.x * objectSprite.bounds.size.y;
                }

                objectPos = new Vector2(objectPos.x+1, objectPos.y);
            }
            objectPos = new Vector2(objectPos.x-mapWidth, objectPos.y-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
