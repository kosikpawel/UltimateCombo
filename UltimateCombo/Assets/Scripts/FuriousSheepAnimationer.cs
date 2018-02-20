using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuriousSheepAnimationer : MonoBehaviour {
    public Transform[] bodyParts;
    public float[] maxLeftRot;
    public float[] maxRightRot;
    public float oneCycleTime;

    private bool direction;
    private float headRotUnit;
    private float legsRotUnit;
    private float headPreRotUnit;
    private float legsPreRotUnit;


    private float timer;
    private float headRot;
    private float legsRot;

	// Use this for initialization
	void Start () {
        timer = 0;
        headPreRotUnit = (maxRightRot[0] - maxLeftRot[0]) / oneCycleTime;
        headRotUnit = headPreRotUnit * Time.deltaTime;
        legsPreRotUnit = (maxRightRot[1] - maxLeftRot[1]) / oneCycleTime;
        legsPreRotUnit = -legsPreRotUnit;
        legsRotUnit = legsPreRotUnit * Time.deltaTime;

        headRot = maxLeftRot[0];
        legsRot = maxRightRot[1];
        //legsRotUnit = -legsRotUnit;


        //Debug.Log(legsRotUnit + " " + headRotUnit);
        bodyParts[0].rotation = Quaternion.Euler(0, 0, headRot);
        bodyParts[1].rotation = Quaternion.Euler(0, 0, legsRot);
        bodyParts[2].rotation = Quaternion.Euler(0, 0, legsRot);

        direction = true;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if( timer >= oneCycleTime)
        {
            timer = 0;
            direction = !direction;
        }
        if (!direction)
        {
            headRotUnit = -(headPreRotUnit * Time.deltaTime);
            legsRotUnit = -(legsPreRotUnit * Time.deltaTime);
        }
        else
        {
            headRotUnit = (headPreRotUnit * Time.deltaTime);
            legsRotUnit = (legsPreRotUnit * Time.deltaTime);
        }
        headRot += headRotUnit;
        legsRot += legsRotUnit;

        bodyParts[0].rotation = Quaternion.Euler(0, 0, headRot);
        bodyParts[1].rotation = Quaternion.Euler(0, 0, legsRot);
        bodyParts[2].rotation = Quaternion.Euler(0, 0, legsRot);
    }
}
