using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "RestKiller")
        {
            Destroy(gameObject);
        }
    }
}
