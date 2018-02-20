using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlayerMovement : MonoBehaviour {
    Rigidbody2D x;
    public float force;
    private PlayerMovement pm;
	// Use this for initialization
	void Start () {
        x = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (pm.control)
        {
            if (Input.GetKey(KeyCode.W))
            {
                x.velocity = (new Vector2(0, force));
            }
            if (Input.GetKey(KeyCode.S))
            {
                x.velocity = (new Vector2(0, -force));
            }
            if (Input.GetKey(KeyCode.A))
            {
                x.velocity = (new Vector2(-force, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                x.velocity = (new Vector2(force, 0));
            }
        }
     }
}
