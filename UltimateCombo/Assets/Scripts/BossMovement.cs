using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {
    public float movementSpeed;
    public GameObject bullet;
    public GameObject killingSpots;
    public int hp;

    int salut;
    GameObject player;
    Rigidbody2D rb;
    

	void Start () {
        hp = 3;
        salut = 0;
        player = GameObject.Find("Player"); //odnajduje GameObject - Player
        rb = GetComponent<Rigidbody2D>();
        ChangeMovementDirection();
        InvokeRepeating("LaunchBullet", 5f, 0.5f);

        Instantiate(killingSpots, new Vector3(-3.5f, Random.Range(-2, 2f), 0), Quaternion.identity, transform);
        Instantiate(killingSpots, new Vector3(Random.Range(-1f, 1f), Random.Range(-2, 2f), 0), Quaternion.identity, transform);
        Instantiate(killingSpots, new Vector3(3.5f, Random.Range(-2, 2f), 0), Quaternion.identity, transform);
    }

    void ChangeMovementDirection()
    {
        Vector2 directionVector = (Vector2)player.transform.position - (Vector2)transform.position;
        directionVector.Normalize();
        rb.velocity = (directionVector* movementSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerKiller")
        {
            ChangeMovementDirection();
        }
    }

    private void LaunchBullet()
    {
        salut++;
        if(salut < 4)
        {
            GameObject x = Instantiate(bullet, transform.position, Quaternion.identity);
            Destroy(x, 5);
        }
        else if(salut > 12)
        {
            salut = 0;
        }
        
    }


    public void ChangeBossHP()
    {
        hp--;
        if (hp < 1)
        {
            GameObject.Find("BubbleGenerator").GetComponent<ComboTaker>().infoGenerator.MakeInfoObject("Boss Defeated");
            Destroy(gameObject, 1);
        }
    }
}
