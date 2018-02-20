using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour {
    public ComboTaker comboTaker;

    public string[] comboNames;
    public int[] comboIndexes;
    private Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        List<string> namesList = new List<string>();
        namesList.Add("Cheats");
        for (int i = 1; i <= comboNames.Length; i++)
        {
            namesList.Add(comboNames[i-1]);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(namesList);
    }

	public void CheatsDropDown()
    {
        int x = dropdown.value;
        if(x != 0)
        comboTaker.ComboEngage(comboIndexes[x - 1]);
    }
}
