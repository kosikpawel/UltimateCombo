using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillingSpot : MonoBehaviour {

    Transform loadingBar;
    public float speed;

    BossMovement boss;
    private bool direction;
    private bool action;
    private Transform trans;
    void Start()
    {
        //
        trans = GetComponent<Transform>();
        boss = GetComponentInParent<BossMovement>();
        transform.SetParent(null); 
        loadingBar = GetComponentsInChildren<Transform>()[1];
        direction = false;
        action = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {
            if (direction && !PlayerMovement.playerWasHit)
            {
                if (loadingBar.localScale.y < 1)
                {
                    loadingBar.localScale = new Vector3(1.14f, loadingBar.localScale.y + (speed * Time.deltaTime), 1);
                }
                else
                {
                    action = false;
                    ComboTaker cT = GameObject.Find("BubbleGenerator").GetComponent<ComboTaker>();
                    boss.ChangeBossHP();
                    cT.SafeBigInfoGeneration("Boss HP "+ boss.hp + "/3   score +5%");
                    cT.score.AddPercentToScore(5);
                    Destroy(gameObject, 1);
                }
            }
            else
            {
                if (loadingBar.localScale.y > 0)
                {
                    loadingBar.localScale = new Vector3(1.14f, loadingBar.localScale.y - (speed * 1.6f * Time.deltaTime), 1);
                }
            }
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            direction = true;
        }
        if(coll.tag == "LoadingSpot")
        {
            trans.position = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-2, 2f), 0);

        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            direction = false;
        }
    }
}
