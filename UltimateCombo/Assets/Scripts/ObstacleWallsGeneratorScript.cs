using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallsGeneratorScript : MonoBehaviour {
    public bool obstacleSpawning;

    public GameObject obstacle;
    public GameObject sawObstacle;
    public float offSet;                 //offSet miejsca spawnu

    //szansa na podwujna przeszkode rosnie liniowo o 2% 
    public int doubleObstacleChance;                        //zaczynajac od "doubleObstacleChance"(szansa w %)
    public int maxDoubleObstacleChance;                     //konczac na "maxDoubleObstacleChance"(szansa w %)
    public int regularIncreasingDubleObstacleChanceTime;    //co kazde "regularIncreasingDubleObstacleChanceTime" (czas w sekundach)

    //predkosc przeszkod rosnie po przekroczeniu granic czasu gry
    public float[] speedIncreasingTimesTab;     //okreslonych w tablicy "speedIncreasingTimesTab"
    public float obstacleSpeed;                 //zaczynajac od "minObstacleSpeed"
    public float maxObstacleSpeed;              //konczac na "maxObstacleSpeed"
    //przyczym predkosc o (maxObstacleSpeed - minObstacleSpeed)/<rozmiar tablicy "speedIncreasingTimesTab">
    private float increasingObstacleSpeedUnit;     //przechowywane w zmiennej "increasingObstacleSpeedUnit"
    private int sittIndex = 0;                     //zmienna przechowujaca aktualny index tablicy "speedIncreasingTimesTab"


    public float obstacleChargingTime;   //czas przez ktory przeszkoda nic nie robi (nabiera koloru)
    public float timeBetweenObstacles;   //czas jaki musi uplynac od czasku kiedy poprzednia przeszkoda zostanie zniszczona do spawnu nowej
    
    Vector3 leftSpawnPoint;
    Vector3 rightSpawnPoint;
    Vector3 leftSpawnPoint2;
    Vector3 rightSpawnPoint2;
    ObstacleMovementScript oMS;
    //zabezpieczenia
    float timer = 0;
    bool safety = true;
    float safetyTime = 0;
    bool allObstaclesAreDestroyed;
    bool sawObstacleMode;
    bool normalObstacleMode;

    void Start () {
        allObstaclesAreDestroyed = false;
        normalObstacleMode = true;
        oMS = obstacle.GetComponent<ObstacleMovementScript>();
        oMS.speed = obstacleSpeed;
        oMS.shadowTime = obstacleChargingTime;

        InvokeRepeating //wywoluje funkcje co okreslony czas w sekundach
            ("IncreaseChanceForDubleObstacle", regularIncreasingDubleObstacleChanceTime, regularIncreasingDubleObstacleChanceTime);  

        //obliczenia miejsc spawnu przeszkod
        float cameraWidth = Camera.main.pixelWidth;
        leftSpawnPoint = new Vector3 ( 0, 0, 10);
        rightSpawnPoint = new Vector3(cameraWidth, 0, 10);
        leftSpawnPoint = Camera.main.ScreenToWorldPoint(leftSpawnPoint);
        rightSpawnPoint = Camera.main.ScreenToWorldPoint(rightSpawnPoint);
        leftSpawnPoint.x = leftSpawnPoint.x + offSet;
        rightSpawnPoint.x = rightSpawnPoint.x - offSet;
        leftSpawnPoint.y = 0;
        rightSpawnPoint.y = 0;
        leftSpawnPoint2.x = leftSpawnPoint.x - 15 * offSet;
        rightSpawnPoint2.x = rightSpawnPoint.x + 15 * offSet;
        //spawnowanie pierwszej przeszkody
        StartCoroutine(Waiting());

        increasingObstacleSpeedUnit = (maxObstacleSpeed - obstacleSpeed) / speedIncreasingTimesTab.Length;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (obstacleSpawning && allObstaclesAreDestroyed)
        {
            if (normalObstacleMode)
            {
                StartCoroutine(Waiting());
                allObstaclesAreDestroyed = false;
            }
            else if (sawObstacleMode)
            {
                SawObstacleSpawning();
                allObstaclesAreDestroyed = false;
            }
        }
        if(sittIndex < speedIncreasingTimesTab.Length && timer > speedIncreasingTimesTab[sittIndex])
        {
                sittIndex++;
                IncreaseObstacleSpeed();
        }
    }
	
	void InstantiateOblsacle() //przyzwanie przeszkody lub 2 przeszkod
    {
        int x = Random.Range( 0, 100);
        if (x <= doubleObstacleChance)      //obydwu stron
        {
            Instantiate(obstacle, leftSpawnPoint, Quaternion.identity, transform);
            Instantiate(obstacle, rightSpawnPoint, Quaternion.identity, transform);
        }
        else
        {
            x = x - doubleObstacleChance;
            if(x < (100- doubleObstacleChance) / 2) //z lewej strony
            {
                Instantiate(obstacle, leftSpawnPoint, Quaternion.identity, transform);
            }
            else //z prawej strony
            {
                Instantiate(obstacle, rightSpawnPoint, Quaternion.identity, transform);
            }
        }
    }

    public void SetSawObstacleMode(bool turnOn)
    {
        if (turnOn)
        {
            normalObstacleMode = false;
            sawObstacleMode = true;
        }
        else
        {
            normalObstacleMode = true;
            sawObstacleMode = false;
        }
        
    }

    void SawObstacleSpawning()
    {
        float time =0;
        int x = Random.Range(2, 9);
        if (x == 9) x = 8;
        int sawQuantity;
        float[] sawXPos;
        float[] sawYPos;
        sawXPos = new float[] { 0 };
        sawYPos = new float[] { 0 };
        switch (x)
        {
            case 2:
                sawQuantity = 2;
                sawXPos = new float[] { -2.5f, 2.5f};
                sawYPos = new float[] { 1.2f, 1.2f };
                break;
            case 3:
                sawQuantity = 2;
                sawXPos = new float[] { -1.3f, 1.3f };
                sawYPos = new float[] { -2, 2 };
                break;
            case 4:
                sawQuantity = 3;
                sawXPos = new float[] { -3.2f, -0.5f, 2.2f };
                sawYPos = new float[] { -0.5f, 1, 2.5f };
                break;
            case 5:
                sawQuantity = 3;
                sawXPos = new float[] { -4, -0.7f, 3.3f };
                sawYPos = new float[] { 1.2f, -0.1f, 0.7f };
                break;
            case 6:
                sawQuantity = 3;
                sawXPos = new float[] { -2.7f, -1, 2.5f};
                sawYPos = new float[] { -1.6f, 2, 0.5f };
                break;
            case 7:
                sawQuantity = 4;
                sawXPos = new float[] { -3, -1, 1, 3 };
                sawYPos = new float[] { -1.4f, 1.4f, -1.4f, 1.4f };
                break;
            case 8:
                sawQuantity = 4;
                sawXPos = new float[] { -3, -3, 3, 3 };
                sawYPos = new float[] { -1.45f, 1.45f, -1.15f, 1.15f };
                break;
            default:
                sawQuantity = 0;
                break;
        }
        GameObject saw;
        for (int i = 0; i < sawQuantity; i++)
        {

            saw = Instantiate(sawObstacle, new Vector3(sawXPos[i], 5, 0), Quaternion.identity, transform);
            time= saw.GetComponent<SawObstacle>().StartObstacle(sawYPos[i]);
        }
        StartCoroutine(SawWaiting(time));
    }
    IEnumerator SawWaiting(float time)
    {
        yield return new WaitForSeconds(time);
        allObstaclesAreDestroyed = true;
    }
    void IncreaseChanceForDubleObstacle() //funkcja zwiekszajaca poziom trudnosci
    {
        if (doubleObstacleChance < maxDoubleObstacleChance)
        {
            doubleObstacleChance += 2;
        }
    }

    void IncreaseObstacleSpeed()
    {
        obstacleSpeed += increasingObstacleSpeedUnit;
        //Debug.Log(Time.time);
    }

    IEnumerator Waiting() //wywoluje przyzwanie przeszkody z opoznieniem
    {
        yield return new WaitForSeconds(timeBetweenObstacles);
        InstantiateOblsacle();
    }
    
    public void CraftNewObstacle() //wywoluje przyzwanie przeszkody z opoznieniem i zabezpieczeniami przed bledami
    {
        
        if (timer - safetyTime > 1) safety = true;
        if (safety)
        {
            oMS.speed = obstacleSpeed;
            safety = false;
            safetyTime = timer;
            allObstaclesAreDestroyed = true;
            
        }
    }
}
