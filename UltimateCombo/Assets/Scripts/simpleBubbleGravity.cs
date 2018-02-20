using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleBubbleGravity : MonoBehaviour {
    public float minSpeed;
    public float maxSpeed;
    public float minSize;
    public float maxSize;
    private float gravityStrength;
    private Rigidbody2D rb;


    void Start()
    {
        gravityStrength = Random.Range(minSpeed, maxSpeed);
        rb = GetComponent<Rigidbody2D>();
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1f);
        rb.velocity = new Vector2(0f, gravityStrength);
    }
}
