using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    public GameObject Missile;
    public GameObject Antimissile;
    public GameObject destroyingLine;
    public GameObject obstacleDestroyingLine;
    public GameObject bubbleDestroyingLine;
    public GameObject boss;
    public GameObject BubbleGen;
    public ComboTaker cT;
    public GameObject healingPoint;
    public GameObject bigTimer;
    public Vector3[] missileRaidSpawnPoints;
    Vector3 missileSpawnPoint;
    float randomMissileSpawnPoint;
    public GameObject mObject;
    public GameObject pObject;
    public GameObject mObjectSmoke;
    public GameObject pObjectSmoke;
    public GameObject mpSmoke;

    void Start() {
        LunchMpObjects();
    }

    void LunchMpObjects() {
        Vector3 pointA = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 10);
        pointA = Camera.main.ScreenToWorldPoint(pointA);
        float x = pointA.x;
        float y = pointA.y;

        mObject = Instantiate(mObject, new Vector3(-x + 2.58635f, y / 2 -.9f, 0f), Quaternion.identity);
        pObject = Instantiate(pObject, new Vector3(x + 0.81365f, y / 2 -.9f, 0f), Quaternion.identity);
    }

    public void ResetAfterMission() {
        mObject.GetComponent<MP_Lunchers>().MpRestart();
        pObject.GetComponent<MP_Lunchers>().MpRestart();
    }

    public void SetMpSmoke(bool letter) {
        if (letter) {
            if(cT.ultimatTaker.m == false) {
                mObjectSmoke = Instantiate(mpSmoke, mObject.transform, false);
                mObjectSmoke.transform.localPosition = new Vector3(-2.6f, 0, 0);
            }
        }
        else {
            if (cT.ultimatTaker.p == false) {
                pObjectSmoke = Instantiate(mpSmoke, pObject.transform, false);
                pObjectSmoke.transform.localPosition = new Vector3(-0.78f, 0, 0);
            }
        }
    }

    public void RemoveMpSmoke() {
        Destroy(mObjectSmoke);
        Destroy(pObjectSmoke);
    }

    public void MpBuble(bool letter) {
        if (letter) {
            mObject.GetComponent<MP_Lunchers>().MpBubble();
        }
        else {
            pObject.GetComponent<MP_Lunchers>().MpBubble();
        }
    }


    public void MissileSpawn ()
    {
        randomMissileSpawnPoint = Random.Range(-6, 6);
        missileSpawnPoint = new Vector3(randomMissileSpawnPoint, -6, 0);
        Instantiate(Missile, missileSpawnPoint, Quaternion.identity, transform);
    }
    public void SpawnAntimissile()
    {
        Instantiate(Antimissile, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-2, 2f), 0), Quaternion.identity);
    }
    public void SpawnHealingPoint()
    {
        GameObject x = Instantiate(healingPoint, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-2, 2f), 0), Quaternion.identity,transform);
        Destroy(x, 60);

    }
    public void SpawnFisrtBoss()
    {
        Instantiate(boss, new Vector3(0, -5,0 ), Quaternion.identity, transform);
    }
    public void MissileRaid( int raidQuantity)
    {
        for(int i = 0; i < raidQuantity; i++)
        {
            Instantiate(Missile, missileRaidSpawnPoints[i], Quaternion.identity, transform);
        }
    }
    public void SpawnDestroyingLine()
    {
        GameObject x = Instantiate(destroyingLine, new Vector3 (0, -5, 0), Quaternion.identity, transform);
        Destroy(x, 7);
    }
    public void SpawnObstacleDestroyingLine()
    {
        GameObject x = Instantiate(obstacleDestroyingLine, new Vector3(0, -5, 0), Quaternion.identity, transform);
        Destroy(x, 7);
    }
    public void SpawnBubbleDestroyingLine()
    {
        GameObject x = Instantiate(bubbleDestroyingLine, new Vector3(0, -5, 0), Quaternion.identity, transform);
        Destroy(x, 7);
    }
}
