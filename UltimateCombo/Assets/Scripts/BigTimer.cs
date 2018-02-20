using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigTimer : MonoBehaviour {
    bool oneActivationProtection;
    Text textObject;
    float timeLeft;

	void Awake () {
        textObject = GetComponent<Text>();
        oneActivationProtection = false;
	}

    public void SetBigTimer(int time, string textToShow = null)
    {
        if ( !oneActivationProtection )
        {
            gameObject.SetActive(true);
            oneActivationProtection = true;
            timeLeft = time;
            if (textToShow != null)
            {
                textObject.text = textToShow;
            }
            else
            {
                textObject.text = timeLeft.ToString();
            }
            StartCoroutine(TurnOff());
        }
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1);
        if( timeLeft > 0 )
        {
            timeLeft--;
            textObject.text = timeLeft.ToString();
            StartCoroutine(TurnOff());
        }
        else
        {
            oneActivationProtection = false;
            gameObject.SetActive(false);
        }    
    }
    //Szybsze zgaszanie napisu
    public void FastEnd()
    {
        oneActivationProtection = false;
        gameObject.SetActive(false);
    }
    
}
