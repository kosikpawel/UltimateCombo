using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneratorScript : MonoBehaviour {

    public GameObject[] obstaclesPrefabs;                 //tablica na prefaby babelkow    
    public float minTimeBeTweenObstacles;
    public float maxTimeBetweenObstacles;                 //maksymalny czas miedzy spawnem kolejnych babelkow


    private int obstaclePrefabsSize;                      //ilosc babelkow w tablicy bubblesPrefabs[]
    private float randomTime;                           //losowy czas mieczy babelkami
    private float timer;                                //stoper??

    private int cameraWidth;


    void Start()
    {
        cameraWidth = Camera.main.pixelWidth;
        randomTime = Random.Range(minTimeBeTweenObstacles, maxTimeBetweenObstacles);       //ustawia pierwszy losowy czas miedzy spawnem babelkow
        timer = 0;                                                  //zeruje stoper
        obstaclePrefabsSize = obstaclesPrefabs.Length;                  //pozyskuje dana ile babelkow jest w tablicy bubblesPrefabs[]
    }

    void Update()
    {
        timer += Time.deltaTime;                                                //Stoper
        if (timer >= randomTime)                                               //jezeli minal czas spawnu
        {
            int randomObstacles = Random.Range(0, obstaclePrefabsSize - 1);          //losuje który babelek zespawnowac
            int x = Random.Range(0, cameraWidth);

            Vector3 randomBubblePos = new Vector3(x, -500f, 10f);
            randomBubblePos = Camera.main.ScreenToWorldPoint(randomBubblePos);
            //randomBubblePos.z -= 1;

            Instantiate(obstaclesPrefabs[randomObstacles], randomBubblePos, Quaternion.identity, transform);

            randomTime = Random.Range(minTimeBeTweenObstacles, maxTimeBetweenObstacles);
            timer = 0;
        }
    }
}

