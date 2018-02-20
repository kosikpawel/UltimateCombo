using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGeneratorScript : MonoBehaviour {
    public bool bubbleSpawning;
    public GameObject[] bubblesPrefabs;                 //tablica na prefaby babelkow
    public float maxTimeBetweenBubbles;                 //maksymalny czas miedzy spawnem kolejnych babelkow
    public float minTimeBetweenBubbles;
    public float[] chanceForBubble;
    public float[] chanceForBubbleCopy;
    public bool cameleonMP = true;

    private int bubblePrefabsSize;                      //ilosc babelkow w tablicy bubblesPrefabs[]
    private float randomTime;                           //losowy czas mieczy babelkami
    private float randomTime2;
    private float timer1;                                //stoper??
    private float timer2;
    private float[] chanceSum; //!!!
    private float sum = 0; //!!!
    private int randomBubble;
    private float xInWorld;
    private float yInWorld;
    private bool yIsRevert;
    public static int bubbleOrderInLayer;


    void Start () {
        bubbleOrderInLayer = 101;
        randomTime = Random.Range(minTimeBetweenBubbles, maxTimeBetweenBubbles);       //ustawia pierwszy losowy czas miedzy spawnem babelkow
        randomTime2 = Random.Range(minTimeBetweenBubbles, maxTimeBetweenBubbles);
        yIsRevert = false;

        Vector3 lowerLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)); //oblicza lewy dolny bok kamery
        xInWorld = lowerLeftCorner.x;
        yInWorld = -5;

        timer1 = 0;                                                  //zeruje stoper
        timer2 = 0.2f;
        bubblePrefabsSize = bubblesPrefabs.Length;                  //pozyskuje dana ile babelkow jest w tablicy bubblesPrefabs[]
        chanceSum = new float[bubblePrefabsSize + 1];   //!!!
        CountingChance();
    }

	void Update () {
        if (bubbleSpawning)
        {
            timer1 += Time.deltaTime;                                                //Stoper
            timer2 += Time.deltaTime;


            if (timer1 >= randomTime)                                               //jezeli minal czas spawnu
            {
                randomBubble = BubbleDraw();          //losuje który babelek zespawnowac


                Instantiate(bubblesPrefabs[randomBubble], new Vector3(Random.Range(xInWorld, 0), yInWorld, 0), Quaternion.identity, transform);
                randomTime = Random.Range(minTimeBetweenBubbles, maxTimeBetweenBubbles);
                timer1 = 0;
                //Debug.Log(Time.timeSinceLevelLoad);
            }

            if (timer2 >= randomTime2)                                               //jezeli minal czas spawnu
            {
                randomBubble = BubbleDraw();          //losuje który babelek zespawnowac

                Instantiate(bubblesPrefabs[randomBubble], new Vector3(Random.Range(0, -xInWorld), yInWorld, 0), Quaternion.identity, transform);
                randomTime2 = Random.Range(minTimeBetweenBubbles, maxTimeBetweenBubbles);
                timer2 = 0;
            }

        }
    }

    void CountingChance()
    {
        sum = 0;
        chanceSum[0] = 0;   //Maciek pozmienial - tego nie bylo
        for (int i = 1; bubblePrefabsSize >= i; i++) //Maciek pozmienial bylo:  (int i = 0; bubblePrefabsSize >= i; i++)
        {
            sum += chanceForBubble[i - 1]; //Maciek pozmienial bylo:  sum += chanceForBubble[i];
            chanceSum[i] = sum;
        }
    }
    int BubbleDraw()
    {
        int bubbleD;
        bubbleD = (int)Random.Range(chanceSum[0], chanceSum[chanceSum.Length - 1]);
        for(int i = 0; chanceSum.Length - 1 > i; i++)
        {
            if(bubbleD >= chanceSum[i] && bubbleD < chanceSum[i+1])
            {
                return i;
                
            }
        }
        return 0;
    }

    public void SetAlternativeMode(bool OnOff, bool spawnBubbleOnTop = false, bool spawnStarsOnly = false, float rain = 1, float[] alternativeChanceForBubble = null)
    {
        
        if (OnOff)
        {
            if (spawnBubbleOnTop)
            {
                yIsRevert = true;
                yInWorld = -yInWorld;
            }
            if (spawnStarsOnly)
            {
                alternativeChanceForBubble = new float[chanceForBubble.Length];
                for (int i = 0; i < chanceForBubble.Length; i++)
                {
                    alternativeChanceForBubble[i] = 0;
                }
                alternativeChanceForBubble[8] = 100;
            }
            
            chanceForBubbleCopy = (float[])chanceForBubble.Clone();
            chanceForBubble = (float[])alternativeChanceForBubble.Clone();
            CountingChance();
        }
        else
        {
            if (yIsRevert)
            {
                yIsRevert = false;
                yInWorld = -yInWorld;
            }
            chanceForBubble = (float[])chanceForBubbleCopy.Clone();
            CountingChance();
        }
    }
}
