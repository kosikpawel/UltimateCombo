using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiki : MonoBehaviour {
    public GameObject pageOne;
    public GameObject pageTwo;
    private bool switcher;

    void Start() {
        switcher = true;
        ChangePage();
    }

    public void ChangePage() {
        pageOne.SetActive(switcher);
        pageTwo.SetActive(!switcher);
        switcher = !switcher;
    }
    
}
