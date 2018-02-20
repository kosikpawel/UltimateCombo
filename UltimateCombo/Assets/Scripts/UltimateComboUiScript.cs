using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateComboUiScript : MonoBehaviour
{
    public Sprite noWheel;
    public Sprite[] bubbleSprites;
    public Image[] imageComponents;
    public int levelCup;
    public int levelCupIncrement;
    public int[] bubblesForUC;
    public int curentPosition;
    public Text leftNumber;
    public ComboTaker ct;
    public int reward;

    public void LoadLevelFullCombo() {
        curentPosition = 0;
        bubblesForUC = new int[levelCup];
        SetLeftNumber();
        reward = levelCup;
        for (int i = 0; i < levelCup; i++) {
            bubblesForUC[i] = RandomIndex();
            //Debug.Log(i);
        }
        SetUI();
    }
    public void SetUI() {
        for(int i=0; i<3; i++) {
            if(curentPosition + i > levelCup-1) {
                imageComponents[i].sprite = noWheel;
            }
            else {
                InsertBubble(bubblesForUC[curentPosition + i] , i);
            }
        }
    }
    int RandomIndex() {
        int x;
        x = Random.Range(0, 5);
        if (x == 3) x = 5;
        return x;
    }
    void InsertBubble(int index, int position) {
        //Debug.Log(index + " " + position);
        imageComponents[position].sprite = bubbleSprites[index];
    }

    public void SetLeftNumber() {
        leftNumber.text = (levelCup - curentPosition).ToString();
    }
}
