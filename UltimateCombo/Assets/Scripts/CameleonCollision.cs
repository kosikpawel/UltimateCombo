using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameleonCollision : MonoBehaviour {
    private bool workOnlyOnce = true;

    public Sprite gold;
    public Sprite red;

    public Sprite radioactive;
    public Sprite m;
    public Sprite p;
    public SpriteRenderer childRenderer;


    public int bubbleIndex;
    public float destructionTime;
    private bool disap = false;
    Color start;
    Color end;
    SpriteRenderer sr;
    Transform trans;
    float timer;

    ComboTaker moveIndex;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Player"  && workOnlyOnce)
        {
            workOnlyOnce = false;
            int x = Random.Range(0, 9);
            if (x == 0)
            {
                GetComponent<SpriteRenderer>().sprite = red;
                childRenderer.sprite = radioactive;
                bubbleIndex = 13;
            }
            else
            {
                if(GetComponentInParent<BubbleGeneratorScript>().cameleonMP == true)
                {
                    //GetComponent<SpriteRenderer>().sprite = gold;
                    childRenderer.sprite = m;
                    bubbleIndex = 7;
                    GetComponentInParent<BubbleGeneratorScript>().cameleonMP = false;
                }
                else
                {
                    //GetComponent<SpriteRenderer>().sprite = gold;
                    childRenderer.sprite = p;
                    bubbleIndex = 8;
                    GetComponentInParent<BubbleGeneratorScript>().cameleonMP = true;
                }
            }
        }
        else if (coll.tag == "DestroyingLine" || coll.tag == "BubbleDestroyingLine")
        {
            Destroy(gameObject);
        }
        else if (coll.tag == "FuriousSheep" && workOnlyOnce)
        {
            workOnlyOnce = false;
            disap = true;
            start = Color.white;
            end = Color.clear;
            sr = GetComponent<SpriteRenderer>();
            trans = GetComponent<Transform>();
            timer = 0;
        }
    }




    void Update()
    {
        if (disap)
        {
            StartCoroutine(Waiter());

            timer += (Time.deltaTime / destructionTime);
            trans.localScale = trans.localScale * (1 + (Time.deltaTime * Mathf.PingPong(destructionTime, 10)));
            sr.color = Color.Lerp(start, end, timer); //
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(destructionTime);
        disap = false;
        Destroy(gameObject);
    }

    public void CallPlayerCollision()
    {
        //workOnlyOnce = false;
        disap = true;
        start = Color.white;
        end = Color.clear;
        sr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        timer = 0;
        moveIndex = GetComponentInParent<ComboTaker>();
        moveIndex.NormalEngage(bubbleIndex);
    }
    public void CallWallCollision()
    {
        Destroy(gameObject);
    }
}
