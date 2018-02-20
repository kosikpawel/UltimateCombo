using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatsSystem : MonoBehaviour {
    // Opis indexow w tablicach
    // [0] - playerSpeed
    // [1] - shieldsCooldown
    // [2] - scoreMultiplier

    public int statsQuantity;               //ile jest statystyk ktore mozna zmienic

    public WheelStatisticsExpose[] exposeStats;         //skrypt do wyswietlania na kole
    public Button[] buttonsToTurnOff;                   //do wylaczania przycisku na max levelu
    
    public StarsShieldsChangerUI stars;     //skrypt zmieniajacy wyswietlana ilosc gwiazdek

    public int[] UpgradesStarsPrices;       //Ilosci gwiazdek ktore trzeba bedzie wydac na kolejne zwiekszenia poziomu statystyk
    public int changingSpeedPrice;


    private int ownedStars;                 //faktyczna ilosc posiadanych gwiazdek
    private int[] pointsInStatistisc;       //ilosci punktow rozdane w statystyki

    private bool available = false;
    private ProgressData dataToLoad;

    private bool somethingWasChanged;       //jezeli zostanie ustawione na true to cos zostalo zmienione i nalezy zapisac zmiany do pliku

	void Start () {
        somethingWasChanged = false;                    //ustawia na wartosc wejsciowa false
        pointsInStatistisc = new int[statsQuantity];    //tworzy tablice na statystyki
        
        LoadData();                                     //wczytuje dane z pliku
        stars.ChangeStars(ownedStars);         //ustawia ilosc gwiazdek (wizualne)
        exposeStats[0].SetStatisticLevelOnWheel(pointsInStatistisc[0]); //wyswietlanie na kole player speed
        exposeStats[0].SetUpgradeStarPrice(changingSpeedPrice.ToString());
        //for ( int i=1; i<statsQuantity; i++)            //wyswietla poziomy na jakich sa kolejne statystyki na kolach (wizualne) (wizualne)
        //{
        //    ExposeStatisticOnUI(i);         //funkcja wyswietlajaca
        //}
        ExposeStatisticOnUI(2);
    }


    void StatUpgrade(int statIndex)
    {
        if (ownedStars >= UpgradesStarsPrices[pointsInStatistisc[statIndex]])
        {
            somethingWasChanged = true;                 //ustawia flage ze bedzie trzeba zapisac zmiany
            ownedStars -= UpgradesStarsPrices[pointsInStatistisc[statIndex]];   //odejmuje gwiazdki
            stars.ChangeStars(ownedStars);     //ustawia nowa ilosc gwiazdek (wizualne)
            pointsInStatistisc[statIndex]++;            //zwieksza poziom umiejetnosci
            ExposeStatisticOnUI(statIndex);             //Funkcja wyswietla zmiany w UI (wizualne)
        }
        else
        {
            //tutaj dzwiek na nope
        }

    }
    void SpeedChange(bool gainMoreSpeed)
    {
        if(ownedStars >= changingSpeedPrice)
        {
            if (gainMoreSpeed && pointsInStatistisc[0] < 8)
            {
                somethingWasChanged = true;
                ownedStars -= changingSpeedPrice;   //odejmuje gwiazdki
                stars.ChangeStars(ownedStars);     //ustawia nowa ilosc gwiazdek (wizualne)

                pointsInStatistisc[0]++;            //zwieksza poziom umiejetnosci
                exposeStats[0].SetStatisticLevelOnWheel(pointsInStatistisc[0]);             //Funkcja wyswietla zmiany w UI (wizualne)
            }
            else if(!gainMoreSpeed && pointsInStatistisc[0] > 0)
            {
                somethingWasChanged = true;
                ownedStars -= changingSpeedPrice;   //odejmuje gwiazdki
                stars.ChangeStars(ownedStars);     //ustawia nowa ilosc gwiazdek (wizualne)

                pointsInStatistisc[0]--;            //zwieksza poziom umiejetnosci
                exposeStats[0].SetStatisticLevelOnWheel(pointsInStatistisc[0]);             //Funkcja wyswietla zmiany w UI (wizualne)
            }
            else
            {
                //tutaj dzwiek na nope
            }
        }
        else
        {
            //tutaj dzwiek na nope
        }
    }

    void ExposeStatisticOnUI(int statIndex) //Funkcja wyswietla aktualne dane w UI (wizualne)
    {
        exposeStats[statIndex].SetStatisticLevelOnWheel(pointsInStatistisc[statIndex]);     //wyswietla nowa ilosc kafelkow adekwatna do nowego levelu

        if (pointsInStatistisc[statIndex] < 8) //dla lvl mniejszego niz 8(max) - wyswietla cene nastepnego poziomu
        {
            exposeStats[statIndex].SetUpgradeStarPrice(UpgradesStarsPrices[pointsInStatistisc[statIndex]].ToString());
        }
        else  //dla (max lvl) - wylacza przycisk i wyswietla ze osiagnieto maksymalny poziom
        {
            buttonsToTurnOff[statIndex].enabled = false;
            exposeStats[statIndex].SetUpgradeStarPrice("Max Level");
        }
    }

    //kazdy przycisk musi miec swoja funkcje
    public void PlayerSpeedButtonPlus()
    {
        SpeedChange(true);
    }
    public void PlayerSpeedButtonMinus()
    {
        SpeedChange(false);
    }
    //kazdy przycisk musi miec swoja funkcje
    public void ShieldCooldownButton()
    {
        StatUpgrade(1);
    }
    //kazdy przycisk musi miec swoja funkcje
    public void ScoreMultiplierButton()
    {
        StatUpgrade(2);
    }
    void LoadData()     //wczytywanie danych z pliku
    {
        if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);

            dataToLoad = (ProgressData)bf.Deserialize(file);
            file.Close();
            available = true;
        }
        if (available)
        {
            pointsInStatistisc[0] = dataToLoad.playerSpeedPoints;    // [0] - playerSpeed
            pointsInStatistisc[2] = dataToLoad.scorMultiplierPoints;    // [2] - scoreMultiplier
            ownedStars = dataToLoad.starsToSave;
        }
    }
    //zostanie wywolana przy zmianie sceny, jezeli zaszly zmiany zapisze je do pliku
    public void SaveData()
    {
        if (somethingWasChanged)
        {
            if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);
                ProgressData dataToSave = new ProgressData();
                dataToSave = (ProgressData)bf.Deserialize(file);
                file.Close();
                dataToSave.playerSpeedPoints = pointsInStatistisc[0];
                dataToSave.scorMultiplierPoints = pointsInStatistisc[2];
                dataToSave.starsToSave = ownedStars;
                File.Delete(Application.persistentDataPath + "/progressData.dat");
                file = File.Create(Application.persistentDataPath + "/progressData.dat");
                bf.Serialize(file, dataToSave);
                file.Close();
            }
            Debug.Log("zmianabyla zapisuje");
        }
    }
}