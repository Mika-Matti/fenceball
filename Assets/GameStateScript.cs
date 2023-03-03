using UnityEngine;
using TMPro;

public class GameStateScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject tileSetter;
    public float gameArea;
    public float fencedArea;
    public float areaCoveredPercent;
    public TMP_Text areaCoveredText;

    // Start is called before the first frame update
    void Start()
    {
        gameArea = tileSetter.GetComponent<TileSetterScript>().area;
        fencedArea = 0;
        UpdateFencedArea(0);

        var ball1 = Instantiate(ball, new Vector2(0, 0), new Quaternion(0, 0, 0, 0));
        ball1.GetComponent<BallScript>().SetVelocity(5, 5);
        var ball2 = Instantiate(ball, new Vector2(7, 3), new Quaternion(0, 0, 0, 0));
        ball2.GetComponent<BallScript>().SetVelocity(5, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateFencedArea(float newArea)
    {
        fencedArea += newArea;
        areaCoveredPercent = fencedArea / gameArea * 100;
        areaCoveredText.text = System.Math.Round(areaCoveredPercent,2) + "%";
    }
}
