using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameleonGravity : MonoBehaviour {

    public float minSpeed;
    public float maxSpeed;
    public float minSize;
    public float maxSize;
    private float gravityStrength;
    private Rigidbody2D rb;


    void Start()
    {
        int cameraWidth = Camera.main.pixelWidth;
        int rand = Random.Range(1, 3);
        Vector3 bubbleNewPos = new Vector3(cameraWidth/3 * rand, -100, 10);
        bubbleNewPos = Camera.main.ScreenToWorldPoint(bubbleNewPos);
        transform.position = new Vector3(bubbleNewPos.x, transform.position.y, 0f);

        gravityStrength = Random.Range(minSpeed, maxSpeed);
        rb = GetComponent<Rigidbody2D>();
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1f);
    }

    void Update()
    {
        rb.velocity = new Vector2(0f, gravityStrength);
    }
}
