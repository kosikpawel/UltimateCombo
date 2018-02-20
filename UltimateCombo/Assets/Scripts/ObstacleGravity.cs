using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGravity : MonoBehaviour {

    //public float minSpeed;
    //public float maxSpeed;
    public float speed;

    private float gravityStrength;
    private Rigidbody2D rb;


    void Start()
    {
        //gravityStrength = Random.Range(minSpeed, maxSpeed);
        rb = GetComponent<Rigidbody2D>();
        int angel = Random.Range(0, 4);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angel * 90));

    }

    void Update()
    {
        rb.velocity = new Vector2(0f, speed);//gravityStrength);
    }
}
