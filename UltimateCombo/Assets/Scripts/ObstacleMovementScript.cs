using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementScript : MonoBehaviour {
    public float speed;                 //public dla wygody zmieniac z ObstacleGenerator
    public float shadowTime;            //public dla wygody zmieniac z ObstacleGenerator
    Rigidbody2D rb;
    SpriteRenderer[] obstaclePartsRenderer;     
    Transform[] obstaclePartsTransform; //do zmiany tagow

    //color Lerp
    float timer = 0;
    Color start;
    Color end;

    bool updateMod = true;
    int randomOff;

    void Awake()
    {
        obstaclePartsRenderer = GetComponentsInChildren<SpriteRenderer>();
        obstaclePartsTransform = GetComponentsInChildren<Transform>();
    }
    void Start () {
        randomOff = Random.Range(0,4);
        obstaclePartsRenderer[randomOff].enabled = false;
        obstaclePartsRenderer[randomOff+1].enabled = false;
        start = obstaclePartsRenderer[0].color;
        end = Color.white;

        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x > 0)
        {
            speed = -speed;
        }
        StartCoroutine(Waiting());
    }
	
	void Update () {
        timer += (Time.deltaTime / shadowTime); //do colorLerp
        if (updateMod) //true - zmienia kolor
        {
            for (int i = 0; i<obstaclePartsRenderer.Length; i++)
            {
                obstaclePartsRenderer[i].color = Color.Lerp(start, end, timer);
            }
        }
        else          //false  - daje predkosc
        {
            rb.velocity = new Vector2(speed, 0);
        }
	}

    public void Destruction()
    {
        timer = 0;
        GetComponentInParent<ObstacleWallsGeneratorScript>().CraftNewObstacle();
        Destroy(gameObject);
    }
    private void ObstacleActivation() // wlaczenie boxcolliderow i pelnego koloru
    {
        for (int i = 0; i < obstaclePartsRenderer.Length; i++)
        { 
            obstaclePartsTransform[i+1].transform.gameObject.tag = "PlayerKillerObstacle";
            obstaclePartsRenderer[i].color = Color.white;
        }
        obstaclePartsTransform[randomOff + 1].gameObject.tag = "InactiveObstacle";
        obstaclePartsTransform[randomOff + 2].gameObject.tag = "InactiveObstacle";
        updateMod = false;
    }

    IEnumerator Waiting() // wlaczenie boxcoliderow i pelnego koloru z opoznieniem
    {
        yield return new WaitForSeconds(shadowTime);
        ObstacleActivation();
    }
}
