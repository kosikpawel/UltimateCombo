using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsShieldsChangerUI : MonoBehaviour {
    public void ChangeStars(int newQuantity)
    {
        GetComponentInChildren<Text>().text = newQuantity.ToString();
    }
}
