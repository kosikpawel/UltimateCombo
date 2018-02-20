using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour {

    private float speed;
    private float rotatingSpeed;
    private GameObject target;
    Rigidbody2D rb;
    private bool itsTime = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(ItsTimetoStap(7));
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rotatingSpeed = Random.Range(120, 520);
        speed = Random.Range(3f, 3.5f);
        }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y > 4)
        {
            Destroy(gameObject);
        }
        else if (target.tag == "Player" && itsTime)
        {
            Vector2 pointToTraget = (Vector2)transform.position - (Vector2)target.transform.position;
            pointToTraget.Normalize();
            float value = Vector3.Cross(pointToTraget, transform.right).z;
            rb.angularVelocity = rotatingSpeed * value;
            rb.velocity = transform.right * speed;
        }
        else
        {
            Vector2 pointToTraget = (Vector2)transform.position - new Vector2(0, 5);
            pointToTraget.Normalize();
            float value = Vector3.Cross(pointToTraget, transform.right).z;
            rb.angularVelocity = rotatingSpeed * value;
            rb.velocity = transform.right * speed;
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Antimissile")
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator ItsTimetoStap(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        itsTime = false;
    }
    
}
