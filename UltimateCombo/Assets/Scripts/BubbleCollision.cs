using System.Collections;
using UnityEngine;

public class BubbleCollision : MonoBehaviour {
    public int bubbleIndex;
    public float destructionTime;
    private bool disap = false;
    Color start;
    Color end;
    SpriteRenderer sr;
    Transform trans;
    float timer;

    bool workOnlyOnce = true;

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        BubbleGeneratorScript.bubbleOrderInLayer++;
        if(BubbleGeneratorScript.bubbleOrderInLayer > 200)
        {
            BubbleGeneratorScript.bubbleOrderInLayer = 101;
        }
        
        sr.sortingOrder = BubbleGeneratorScript.bubbleOrderInLayer;
        GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = BubbleGeneratorScript.bubbleOrderInLayer;
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if ( coll.tag == "RestKiller" || coll.tag == "DestroyingLine" || coll.tag == "BubbleDestroyingLine")
        {
            Destroy(gameObject);
        }
        else if (coll.tag == "Player" && workOnlyOnce)
        {
            workOnlyOnce = false;
            disap = true;
            start = Color.white;
            end = Color.clear;
                
            trans = GetComponent<Transform>();
            timer = 0;

            GetComponentInParent<UltimateTaker>().ComboElement(bubbleIndex); //test
            //if (!UltimateTaker.ultimateCombo)
            //{
            //    GetComponentInParent<ComboTaker>().ComboElement(bubbleIndex);
            //}
            //else
            //{
            //    GetComponentInParent<UltimateTaker>().ComboElement(bubbleIndex);
            //}
                
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

            GetComponentInParent<ComboTaker>().score.AddToScore(100);
            GetComponentInParent<ComboTaker>().infoGenerator.MakeInfoObject("+100 points");
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
}
