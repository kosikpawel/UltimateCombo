using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuServer : MonoBehaviour
{   public Text TextStars;
    public Text TextScore;
    private ProgressData dataToLoad;            //referencja do skryptu 
    private bool avalable = false;              //do sprawdzania czy dane sa dostepne

    void Awake()
    {

        //jeżeli istnieje plik z zapisanymi danymi to otworz go, pobierz z niego dane
        //i ustaw że dane sa dostepne do pobrania
        if (File.Exists(Application.persistentDataPath + "/progressData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/progressData.dat", FileMode.Open);

            dataToLoad = (ProgressData)bf.Deserialize(file);
            file.Close();
            avalable = true;
            Load();
        }
    }
    void Load()
    {
        if(avalable)
        {
            String fullText = "";
            String fullTex2 = "";
            fullText = dataToLoad.starsToSave.ToString();
            TextStars.text = fullText;
            fullTex2 = dataToLoad.scoreToSave.ToString();
            TextScore.text = fullTex2;
        }  
    }
    public void ResetSave()
    {
        File.Delete(Application.persistentDataPath + "/progressData.dat");  //usowa plik z danymi
        SceneManager.LoadScene(0);                                          //i ponownie wczytuje strone menu
    }
}