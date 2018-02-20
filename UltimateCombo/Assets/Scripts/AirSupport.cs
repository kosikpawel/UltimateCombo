using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSupport : MonoBehaviour {
    public int index; //index do ComboTakera - dziala przez NormalEngage(index)

    Rigidbody2D rB;
    float rotationSpeed; //predkosc rotacji

    private bool workOnlyOnce = true; //do kolizji zabezpieczenie
    void Start () {
        rB = GetComponent<Rigidbody2D>();
        rB.velocity = new Vector2(0, -0.5f); //nadaje predkosc do dolu predkosc zostanie niezmienna do zniszczenia obiektu
        rotationSpeed = 15;                     //ustalenie predkosci rotacji
        rB.angularVelocity = rotationSpeed;     //pierwsze nadanie predkosci rotacji
    }
	
	void Update () {
        if(!((transform.eulerAngles.z > 340 && transform.eulerAngles.z < 360) || (transform.eulerAngles.z >= 0 && transform.eulerAngles.z < 20)))
            //jezeli wychylenie nie nalezy do przedzialu (-20,20) stopni
        {
            rotationSpeed = -rotationSpeed; //zmienia kierunek predkosci rotacji
            rB.angularVelocity = rotationSpeed;
        }
	}



    void OnTriggerEnter2D(Collider2D coll)
    {
        if ( coll.tag == "RestKiller" )
        {
            Destroy(gameObject);
        }
        else if (coll.tag == "Player" && workOnlyOnce)
        {
            workOnlyOnce = false;

            GetComponentInParent<ComboTaker>().NormalEngage(index);
            Destroy(gameObject);
        }
    }
}
