using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameleonChildCollision : MonoBehaviour {

    

    bool workOnlyOnce = true;

    

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ( coll.tag == "RestKiller" || coll.tag == "DestroyingLine" || coll.tag == "BubbleDestroyingLine")
        {
            GetComponentInParent<CameleonCollision>().CallWallCollision();
        }
        else if (coll.name == "Player" && workOnlyOnce)
        {
            workOnlyOnce = false;
            GetComponentInParent<CameleonCollision>().CallPlayerCollision();
        }
    }

    public void ChangeMyTag(string x)
    {
        transform.gameObject.tag = x;
    }

    
}
