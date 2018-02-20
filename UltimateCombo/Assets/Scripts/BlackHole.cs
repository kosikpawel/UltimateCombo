using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
    public float destructionTime;
    public float rotationSpeed;

    private float actRotation;
    private Transform trans;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        actRotation = trans.localRotation.z;
        StartCoroutine(DestructionAfterTime());
    }
	
	// Update is called once per frame
	void Update () {
        actRotation += (rotationSpeed * Time.deltaTime);
        trans.localRotation = Quaternion.Euler(0,0,actRotation);
	}
    IEnumerator DestructionAfterTime()
    {
        yield return new WaitForSeconds(destructionTime);
        InfoGenerator iG = GameObject.FindGameObjectWithTag("GameInfoGenerator").GetComponent<InfoGenerator>();
        iG.MakeInfoObject("Black Hole End");
        Destroy(gameObject);
    }
}
