using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSheepMovement : MonoBehaviour {
    public float speed;
    public float endXposition;
    private Rigidbody2D rb;

	void Start () {
        transform.position = new Vector3(12, 0, 0);
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if( transform.position.x <= endXposition)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
	}
}
