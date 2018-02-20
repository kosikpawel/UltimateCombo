using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    public float time;

    RectTransform rt;
    float speed;
    float x = 608.5f;

    void Start () {
        speed = x;
        speed = speed / time;
        rt = GetComponent<RectTransform>();

        rt.transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<Image>().enabled = true;
    }
	
	void Update () {
            rt.transform.localPosition = new Vector3(0, rt.transform.localPosition.y + (speed * Time.deltaTime), 0);
            if ( rt.transform.localPosition.y >= x )
            {
                Destroy(gameObject);
            }
    }

}
