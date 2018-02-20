using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoGenerator : MonoBehaviour {
    public GameObject InfoUnit;
    private string textToShow;

    //void Start()
    //{
    //    MakeInfoObject("TylkoTestyje");
    //}
    
    public void MakeInfoObject(string text)
    {
        textToShow = text;
        var x = Instantiate(InfoUnit);
        x.transform.SetParent(gameObject.transform);
    }

    public string ChildNeedsText()
    {
        return textToShow;
    }
}
