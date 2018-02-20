using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUnit : MonoBehaviour {
    public float OnTime;
    public float OffTime;
    public float xRange;
    public float yRange;

    bool mode;
    Color noColor;
    Color colorCopy;
    Text textEffects;
    float timer;

    void Start () {
        textEffects = GetComponent<Text>();
        textEffects.text = GetComponentInParent<InfoGenerator>().ChildNeedsText();
        noColor = Color.clear;
        colorCopy = textEffects.color;
        textEffects.color = noColor;

        GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-xRange,xRange), Random.Range(-yRange, yRange), 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        mode = true;
        timer = 0;

        StartCoroutine( FirstMode() );
    }
	
	void Update () {
        if( mode )
        {
            timer += (Time.deltaTime / OnTime);
            textEffects.color = Color.Lerp(noColor, colorCopy, timer);
        }
        else
        {
            timer += (Time.deltaTime / OffTime);
            textEffects.color = Color.Lerp(colorCopy, noColor, timer);
        } 
    }


    IEnumerator FirstMode()
    {
        yield return new WaitForSeconds(OnTime);
        mode = false;
        timer = 0;
        StartCoroutine( SecondMode() );
    }
    IEnumerator SecondMode()
    {
        yield return new WaitForSeconds(OffTime);
        Destroy(gameObject);
    }
}
