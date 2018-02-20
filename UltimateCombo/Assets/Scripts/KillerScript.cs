using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerScript : MonoBehaviour {
    public GameObject killingPlayerWall;
    public GameObject killingPlayerWallHori;
    //public GameObject killingRestWall;
    public float offSet;

    void Start () {
        Vector3 pointA = new Vector3(Camera.main.pixelWidth, 0f, 10);
        pointA = Camera.main.ScreenToWorldPoint(pointA);
        float x = pointA.x;
        pointA = new Vector3(0f, Camera.main.pixelHeight, 10);
        pointA = Camera.main.ScreenToWorldPoint(pointA);
        float y = pointA.y;

        Instantiate(killingPlayerWall, new Vector3(0f, y + 0.590803f + offSet, 0f), Quaternion.identity, transform);
        Instantiate(killingPlayerWall, new Vector3(0f, -y - 0.590803f - offSet, 0f), Quaternion.identity, transform);
        Instantiate(killingPlayerWallHori, new Vector3(x + 0.51474f + offSet, 0f, 0f), Quaternion.Euler(0, 0, 90f), transform);
        Instantiate(killingPlayerWallHori, new Vector3(-x - 0.51474f - offSet, 0f, 0f), Quaternion.Euler(0, 0, 90f), transform);

        //Instantiate(killingRestWall, new Vector3(0f, 3*y, 0f), Quaternion.identity, transform);
    }

    

}
