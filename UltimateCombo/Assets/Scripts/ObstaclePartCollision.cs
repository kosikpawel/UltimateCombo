using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePartCollision : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Cutlery" || coll.tag == "ObstacleDestroyingLine" || coll.tag == "DestroyingLine")
        {
            gameObject.SetActive(false); 
        }
    }
}
