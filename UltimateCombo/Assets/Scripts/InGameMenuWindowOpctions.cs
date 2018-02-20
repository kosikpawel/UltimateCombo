using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuWindowOpctions : MonoBehaviour {
    public GameObject Es;

	public void NoClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void YesClicked()
    {
        Time.timeScale = 1;
        Es.SetActive(true);
        //Es.GetComponent<EndScene>().enabled = true;
    }
}
