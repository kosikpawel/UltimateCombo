using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntimissileProtec : MonoBehaviour {
    private void Awake()
    {
        Destroy(gameObject, 60);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Missile")
        {
            Destroy(gameObject);
        }
    }
}
