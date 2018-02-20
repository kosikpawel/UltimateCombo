using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuButton : MonoBehaviour {
    public GameObject MenuWindow;

    public void PauseGame()
    {
        MenuWindow.SetActive(true);
        Time.timeScale = 0;
    }
}
