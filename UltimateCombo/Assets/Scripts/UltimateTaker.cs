using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateTaker : MonoBehaviour {
    public static bool missionLunched;
    private ComboTaker comboTaker;          //referencja do pierwotnego ComboTakera
    public GameObject uC_MissionUiObject;   //referencja do calej wizualnej obslugi
    public GameObject uC_MissionText;       //referencja do do napisu wyswietlanego kiedy misja nie jest aktywna
    private UltimateComboUiScript uC_MissionUiScript;   //skrocona referencja do skrypty wizualnej obslugi

    public bool m;
    public bool p;

    void Start () {
        uC_MissionUiScript = uC_MissionUiObject.GetComponent<UltimateComboUiScript>();
        comboTaker = GetComponent<ComboTaker>();
        m = false;
        p = false;
    }

    public void ComboElement(int index) {
            if (missionLunched) {
                if (index - 1 == uC_MissionUiScript.bubblesForUC[uC_MissionUiScript.curentPosition]) {
                    if (uC_MissionUiScript.curentPosition + 1 > uC_MissionUiScript.levelCup - 1) {
                        //nagroda za ukonczenie misji i zakonczenie misji
                        MissionComplited();
                    }
                    else {
                        uC_MissionUiScript.curentPosition++;
                        uC_MissionUiScript.SetLeftNumber();
                        uC_MissionUiScript.SetUI();
                    }
                }
            }
        comboTaker.NormalEngage(index);
    }

    public void MpLetters(bool letter) {
        if (letter) {
            comboTaker.spawner.SetMpSmoke(true);
            m = true;
        }
        else {
            comboTaker.spawner.SetMpSmoke(false);
            p = true;
        }
        if(m && p && !missionLunched) {
            BeginMission();
        }
    }

    //rozpoczyna misje
    public void BeginMission() {
        missionLunched = true;
        comboTaker.SafeBigInfoGeneration("mission");
        uC_MissionUiScript.LoadLevelFullCombo();
        uC_MissionUiObject.SetActive(true);
        uC_MissionText.SetActive(false);
    }
    //konczy misje
    void MissionComplited() {
        comboTaker.SafeBigInfoGeneration("mission complited\n+5000points");
        comboTaker.score.AddToScore(5000);
        uC_MissionUiObject.SetActive(false);
        uC_MissionText.SetActive(true);
        missionLunched = false;
        comboTaker.spawner.ResetAfterMission();
        comboTaker.spawner.RemoveMpSmoke();
        m = false;
        p = false;
    }
}
