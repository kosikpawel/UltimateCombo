using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMovement : MonoBehaviour {
    public float spawnX;
    public float spawnY;
    public float ufoSpeed;
    public float rotationSpeed;
    public float maxAngle;
    public float destructionX;
    public float timeBetweenSlimes;
    public GameObject slime;

    private float timer;
    private float ufoRotZ;
    private Transform ufoTrans;
    private Rigidbody2D rB;
    private Vector2 ufoVelocity;
    private float rotationUnit;
    private bool collisionWorksOnlyOnce;
    private float angelScaler;
	// Use this for initialization
	void Start () {
        collisionWorksOnlyOnce = true;
        timer = 0;
        ufoTrans = GetComponent<Transform>();
        ufoTrans.position = new Vector3(spawnX, spawnY);
        rB = GetComponent<Rigidbody2D>();
        
        rB.angularVelocity = rotationSpeed;
        ufoVelocity = new Vector2(ufoSpeed, 0);
        rB.velocity = ufoVelocity;
        Destroy(gameObject, 18);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        
        angelScaler = ufoTrans.eulerAngles.z;
        if (angelScaler > 300)
        {
            angelScaler = 360 - angelScaler;
        }
        if (angelScaler > maxAngle)
        {
            rotationSpeed = -rotationSpeed;
            rB.angularVelocity = rotationSpeed;
        }
        if (timer > timeBetweenSlimes)
        {
            timer = 0;
            Instantiate(slime, transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity);
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && collisionWorksOnlyOnce)
        {
            collisionWorksOnlyOnce = false;
            GameObject iG = GameObject.FindGameObjectWithTag("GameInfoGenerator");
            iG.GetComponent<InfoGenerator>().MakeInfoObject("UFO Touched +1000 Points");
            GameObject score = GameObject.FindGameObjectWithTag("ScoreInGame");
            score.GetComponent<ScoreScript>().AddToScore(1000);
        }
    }

}
