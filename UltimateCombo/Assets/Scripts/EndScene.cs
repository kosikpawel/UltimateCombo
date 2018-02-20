using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {
    public float time;
    public int nextSceneIndex;

    bool doOnlyOnce = true;
    RectTransform rt;
    float speed;
    float x = 608.5f;

    void Start () {
        rt = GetComponent<RectTransform>();
        rt.transform.localPosition = new Vector3(0, -x, 0);
        GetComponent<Image>().enabled = true;
    }
	
	void Update () {
        if( doOnlyOnce)
        {
            speed = 608.5f;
            speed = speed / time;
        }
        
        rt.transform.localPosition = new Vector3(0, rt.transform.localPosition.y + (speed * Time.deltaTime), 0);
        
        if (rt.transform.localPosition.y >= 0)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
