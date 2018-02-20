using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelStatisticsExpose : MonoBehaviour {
    public GameObject statsLevel;
    public float rotOffSet;
    public Text upgradePriceInfo;

    private float rotUnit;
	// Use this for initialization
    
    public void SetStatisticLevelOnWheel( int CurrentLevel )
    {
        ClearStatLevelWheel();
        for (int i = 0; i<CurrentLevel; i++)
        {
            var levelUnit = Instantiate(statsLevel);
            levelUnit.transform.SetParent(transform);
            RectTransform levelUnitRT = levelUnit.GetComponent<RectTransform>();
            levelUnitRT.localScale = transform.localScale;
            levelUnitRT.position = transform.position;
            levelUnitRT.rotation = Quaternion.Euler(0, 0, rotOffSet - (i * 45));
        }
    }

    void ClearStatLevelWheel()
    {
        foreach ( Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void SetUpgradeStarPrice(string newCost)
    {
        upgradePriceInfo.text = newCost;
    } 
}
