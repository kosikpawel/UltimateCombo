using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BigInfoGenerator : MonoBehaviour {
    public float speed;
    public Text info;
    public RectTransform rt;
    private bool avalable;
    private bool stop;
    private float y;
    private float x;

    void Start () {
        rt = GetComponentInChildren<RectTransform>();
        avalable = true;
        stop = false;
        y = rt.transform.localPosition.y;
        x = rt.transform.localPosition.x;
        SetInfo("Good luck!"); //Good luck!

    }
    public bool SetInfo(string text) {
        if( avalable ) {
            info.text = text;
            rt.transform.localPosition = new Vector3(x, y, 0);
            StartCoroutine("TextEnd");
            StartCoroutine("TextStopSoon");
            avalable = false;
            stop = false;
        }
        return !avalable;
    }
	
	void Update () {
        if (!avalable && !stop) {
            float x = rt.transform.localPosition.x - speed * Time.deltaTime;
            rt.transform.localPosition = new Vector3(x, y, 0);
        }
	}
    IEnumerator TextEnd() {
        yield return new WaitForSeconds(2.3f);
        avalable = true;
    }
    IEnumerator TextStop() {
        yield return new WaitForSeconds(0.52f);
        stop = !stop;
    }
    IEnumerator TextStopSoon() {
        yield return new WaitForSeconds(0.58f);
        stop = !stop;
        StartCoroutine("TextStop");
    }
}
