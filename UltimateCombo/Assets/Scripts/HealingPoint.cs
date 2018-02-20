using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour {
    Transform loadingBar;
    public float speed;

    private bool direction;
    private bool action;
	// Use this for initialization
	void Start () {
        loadingBar = GetComponentsInChildren<Transform>()[1];
        direction = false;
        action = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (action)
        {
            if (direction && !PlayerMovement.playerWasHit)
            {
                if (loadingBar.localScale.y < 1)
                {
                    loadingBar.localScale = new Vector3(1, loadingBar.localScale.y + (speed * Time.deltaTime), 1);
                }
                else
                {
                    action = false;
                    ComboTaker cT = GetComponentInParent<Spawning>().cT;


                    if (GameObject.Find("Player").GetComponent<PlayerHPSystem>().AddSuperShield() == 1)
                    {
                        cT.infoGenerator.MakeInfoObject("ExtraShield");
                    }
                    else
                    {
                        cT.infoGenerator.MakeInfoObject("score +5%");
                        cT.score.AddPercentToScore(5);
                    }
                    Destroy(gameObject, 1);
                }
            }
            else
            {
                if (loadingBar.localScale.y > 0)
                {
                    loadingBar.localScale = new Vector3(1, loadingBar.localScale.y - (speed * 1.6f * Time.deltaTime), 1);
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
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            direction = false;
        }
    }
}
