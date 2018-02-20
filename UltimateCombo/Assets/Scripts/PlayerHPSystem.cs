using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSystem : MonoBehaviour {
    int callBoss;
    public float basicPlayerSpeed;
    public float maxedPlayerSpeed;
    public float basicScoreMultiplier;
    public float maxedScoreMultiplier;
    public int[] extraShieldStarsPrices;
    public Text currentShieldPrice;
    public ComboTaker comboTaker;


    //normalShield jest zawsze ostatnim elementam tablicy tarcz !!!!!!
    public SpriteRenderer shieldSpriteRenderer;
    public StarsShieldsChangerUI StarsUi;
    public static bool endGame = false;
    public bool godMode = false; //jezeli jest wlaczony player nie bedzie umierac
    public GameObject shadowScreen;
    public ScoreScript scoreObject;
    public InfoGenerator infoGenerator;

    public int hp;

    private int currentShieldStarsPriceLevel;
    private int ownedStars = 0;
    private int playerSpeedPoints = 0;
    private int scorMultiplierPoints = 0;
    private ProgressData dataToLoad;            //referencja do skryptu 
    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);

            dataToLoad = (ProgressData)bf.Deserialize(file);
            file.Close();

            ownedStars = dataToLoad.starsToSave;
            playerSpeedPoints = dataToLoad.playerSpeedPoints;     //nalezy wczytac dane z pliku
            scorMultiplierPoints = dataToLoad.scorMultiplierPoints;   //nalezy wczytac dane z pliku
        }
        SetStats();
    }
    void Start()
    { 
        callBoss = 0;
        currentShieldStarsPriceLevel = 0;
        hp = 1;
        currentShieldPrice.text = extraShieldStarsPrices[0].ToString();
        StarsUi.ChangeStars(ownedStars);
    }

    public int AddSuperShield()
    {
        if( hp < 1 )
        {
            shieldSpriteRenderer.enabled = true;
            hp++;
            return 1;
        }
        return 0;
    }

    public void BuyShield()
    {
        if(ownedStars >= extraShieldStarsPrices[currentShieldStarsPriceLevel] && hp==0)
        {
            AddSuperShield();
            ChangeOwnedStars(-extraShieldStarsPrices[currentShieldStarsPriceLevel]);
            if (currentShieldStarsPriceLevel < extraShieldStarsPrices.Length - 1)
            {
                currentShieldStarsPriceLevel++;
                currentShieldPrice.text = extraShieldStarsPrices[currentShieldStarsPriceLevel].ToString();
            }
        }
        else
        {
            if(hp != 0)
            {
                infoGenerator.MakeInfoObject("You don't need shield.");
            }
            else
            {
                infoGenerator.MakeInfoObject("You can't afford this.");
            }
        }
    }

    void SetStats()
    {
        GetComponent<PlayerMovement>().SetSpeedStat(basicPlayerSpeed + (playerSpeedPoints * ((maxedPlayerSpeed - basicPlayerSpeed) / 8)));
        GetComponent<TemporaryPlayerMovement>().force = basicPlayerSpeed + (playerSpeedPoints * ((maxedPlayerSpeed - basicPlayerSpeed) / 8));//tylko na komputer do usuniecia
        scoreObject.scoreMultyplier = basicScoreMultiplier + (scorMultiplierPoints * ((maxedScoreMultiplier - basicScoreMultiplier) / 8));
        scoreObject.MakeScoreMultyplierCopy();
    }

    public void ChangeOwnedStars(int x)
    { 
        ownedStars += x;
        StarsUi.ChangeStars(ownedStars);
    }

    public bool ChangeHP(int x)
    {
        bool playerIsAlive = true;
        hp += x;
        if( godMode && hp < 0) //trzeba usunac przy eksporcie
        {
            hp++;
            Debug.Log("PlayerHPSystem: GodMode jest aktywny, odnawiam hp.");
        }
        if (hp < 0)
        {
            //scoreObject.StopScore();
            playerIsAlive = false;
            ThisIsTheEnd();
        }
        return playerIsAlive;
    }

    void ThisIsTheEnd()
    {
        Time.timeScale = 1;
        endGame = true;
        SejwikBreivik();
        shadowScreen.SetActive(true);
    }

    public bool PlayerIsHit()
    {
        callBoss++;
        if(callBoss > 24)
        {
            comboTaker.ComboEngage(1310000);
            callBoss = 0;
        }
        bool playerIsAlive = ChangeHP(-1);
        if (playerIsAlive)
        {
            if (shieldSpriteRenderer.enabled)
            {
                shieldSpriteRenderer.enabled = false;
            }
        }
        return playerIsAlive;
    }

    private void SejwikBreivik()
    {
        int score = scoreObject.StopScore();//1int.Parse(scoreObject.text);
        int stars = ownedStars;

        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);
            ProgressData dataToSave = new ProgressData();
            dataToSave = (ProgressData)bf.Deserialize(file);
            file.Close();
                if(dataToSave.scoreToSave < score)
                {
                    dataToSave.scoreToSave = score;
                }
                dataToSave.starsToSave = stars; 
                File.Delete(Application.persistentDataPath + "/progressData.dat");
                file = File.Create(Application.persistentDataPath + "/progressData.dat");
                bf.Serialize(file, dataToSave);
                file.Close();
        }
        else
        {
            FileStream file = File.Create(Application.persistentDataPath + "/progressData.dat");
            ProgressData dataToSave = new ProgressData();
            dataToSave.scoreToSave = score;
            dataToSave.starsToSave = stars;
            bf.Serialize(file, dataToSave);
            file.Close();
        }
    }
}
[Serializable]
public class ProgressData
{
    public int scoreToSave = 0;
    public int starsToSave = 0;
    public int playerSpeedPoints = 0;         
    public int scorMultiplierPoints = 0;  
}