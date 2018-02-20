using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollision : MonoBehaviour {
    public float procentPlayerSpeedAfterCollision;
    public float slowTime;
    private bool worksOnlyOnce;
    
    void Start()
    {
        worksOnlyOnce = true;
        StartCoroutine(Destroyer());
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && worksOnlyOnce)
        {
            worksOnlyOnce = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().PlayerSpeedEffects(procentPlayerSpeedAfterCollision, slowTime, true);
        }
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
