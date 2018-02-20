using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    public float movementSpeed;

    GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.Find("Player"); //odnajduje GameObject - Player
        rb = GetComponent<Rigidbody2D>();

        Vector2 directionVector = (Vector2)player.transform.position - (Vector2)transform.position;
        directionVector.Normalize();
        rb.velocity = (directionVector * movementSpeed);
    }

}
