using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFeelerColl : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "JumpWall")
        {
            GetComponentInParent<ObstacleMovementScript>().Destruction();
        }
    }

}
