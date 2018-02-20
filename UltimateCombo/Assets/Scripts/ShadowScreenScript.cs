using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowScreenScript : MonoBehaviour {
    public float timeToMaxTransparent;
    public float maxTransparent;
    public GameObject EndGameScreen;

    private Image shadow;
    private float transparentSpeed;
    private Color shadowLevels;
    private bool onlyOnce;

    void Start()
    {
        shadow = GetComponent<Image>();
        transparentSpeed = (maxTransparent / timeToMaxTransparent) * Time.deltaTime;
        onlyOnce = true;
    }

    void Update () {
		if (shadow.color.a < maxTransparent)
        {
            //Debug.Log(shadow.color.a);
            shadowLevels.a = shadow.color.a + transparentSpeed;
            shadow.color = shadowLevels;
        }
        else if (onlyOnce)
        {
            EndGameScreen.SetActive(true);
            onlyOnce = false;
        }

	}
}
