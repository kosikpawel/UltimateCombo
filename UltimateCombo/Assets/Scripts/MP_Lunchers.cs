using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_Lunchers : MonoBehaviour {
    public UltimateTaker ultimateTaker;
    public GameObject loadingBar;
    public float speed;
    public bool letter;

    public float timeUntilDrop;

    private bool contact;
    private int phase;

    void Start() {
        ultimateTaker = FindObjectOfType<UltimateTaker>();
        phase = 0;
        contact = false;
        UltimateTaker.missionLunched = false;
    }


    void Update() {
        if (!UltimateTaker.missionLunched) {
            if (!PlayerMovement.playerWasHit && contact) {
                phase = 1;
            }
            else if (phase == 1) {
                phase = 0;
                StartCoroutine("WaitUntilDrop");

            }
            switch (phase) {
                case 1:
                    PutUp();
                    break;
                case 2:
                    Drop();
                    break;
                default:
                    break;
            }
        }
    }

    void Drop() {
        if (loadingBar.transform.localScale.y < -0.0f ) {
            loadingBar.transform.localScale = new Vector3(0.2f, loadingBar.transform.localScale.y + (2 *speed * Time.deltaTime), 1);
            phase = 2;
        }
    }

    void PutUp() {
        if (loadingBar.transform.localScale.y > -1.1f ) {
            
            loadingBar.transform.localScale = new Vector3(0.2f, loadingBar.transform.localScale.y - (speed * Time.deltaTime), 1);
        }
        else {
            LetterActivation();
            phase = 0;
        }
    }

    public void MpRestart() {
        loadingBar.transform.localScale = new Vector3(0.2f, -0.9f, 1);
        UltimateTaker.missionLunched = false;
        phase = 2;
        contact = false;
    }
    void LetterActivation() {
        ultimateTaker.MpLetters(letter);
        StopAllCoroutines();
    }

    public void MpBubble() {
        if (!UltimateTaker.missionLunched) {
            phase = 0;
            loadingBar.transform.localScale = new Vector3(0.2f, -1.1f, 1);
            LetterActivation();
        }
    }

    IEnumerator WaitUntilDrop() {
        yield return new WaitForSeconds(timeUntilDrop);
        phase = 2;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "Player") {
            contact = true;
            StopAllCoroutines();
        }
    }
    void OnTriggerExit2D(Collider2D coll) {
        if (coll.tag == "Player") {
            contact = false;
        }
    }
}
