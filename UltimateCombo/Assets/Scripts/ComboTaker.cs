using System.Collections;
using UnityEngine;

public class ComboTaker : MonoBehaviour {
    public ScoreScript score;
    public GameObject player;
    public GameObject FuriousSheep;
    public InfoGenerator infoGenerator;
    public GameObject blackHole;
    public GameObject ufo;
    public GameObject megaBubble;
    public Spawning spawner;
    public ObstacleWallsGeneratorScript obstacleGenerator;
    public GameObject MysteryAirSupport;
    public GameObject ShieldAirSupport;
    public GameObject ultimateComboUI;
    public UltimateTaker ultimatTaker;
    public BigInfoGenerator bigInfoGenerator;

    int missileRaidDifficulty = 1;

    void Start() {
        ultimatTaker = GetComponent<UltimateTaker>();
        InvokeRepeating("ChallengeIsClose", 240, 240);
    }
    void ChallengeIsClose() {
        SafeBigInfoGeneration("5min Challenge");
        StartCoroutine("ChallengeSoon");
    }
    void Challenge() {
        int x = Random.Range(0, 25);
        if( x < 3) {
            x = 7;
        }
        else if ( x < 8) {
            x = 14;
        }
        else if (x < 10) {
            x = 17;
        }
        else {
            x = 10;
        }
        ComboEngage(x);
    }
  

    public void NormalEngage(int index)
    {
        switch (index)
        {
            case 1: //serce
                int random = Random.Range(1, 3);
                if(random == 1) {               //Mystery Air Support
                    infoGenerator.MakeInfoObject("Mystery Air Support");
                    float xMysteryAirSupport = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
                    Instantiate(MysteryAirSupport, new Vector3(Random.Range(xMysteryAirSupport, -xMysteryAirSupport), 5, 0),
                        Quaternion.identity, transform);
                }
                else {                          //Shield Air Support
                    infoGenerator.MakeInfoObject("Shield Air Support");
                    float xShieldAirSupport = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
                    Instantiate(ShieldAirSupport, new Vector3(Random.Range(xShieldAirSupport, -xShieldAirSupport), 5, 0),
                        Quaternion.identity, transform);
                }
                break;

            case 2: //sun
                infoGenerator.MakeInfoObject("+200 score");
                score.AddToScore(200);
                break;
            case 4: //clock
                infoGenerator.MakeInfoObject("+200 score");
                score.AddToScore(200);
                break; 
            case 3: //gearwheel
                int randomGearwheel = Random.Range(1, 3);
                if (randomGearwheel == 1) {     //Healing Point
                    infoGenerator.MakeInfoObject("Healing Point");
                    spawner.SpawnHealingPoint();
                }
                else {                          //Antimissile
                    score.AddToScore(100);
                    infoGenerator.MakeInfoObject("Antimissile");
                    spawner.SpawnAntimissile();
                }
                break;
            case 5: //boom
                int randomBoom = Random.Range(0, 34);
                if(randomBoom < 2) {            //Stars Festival
                    infoGenerator.MakeInfoObject("Stars Festival");
                    GetComponent<BubbleGeneratorScript>().SetAlternativeMode(true, false, true);
                    StartCoroutine(StopEffect(15, 100200030));
                }
                else if (randomBoom < 4) {            //Extra Points
                    score.AddPercentToScore(5);
                    infoGenerator.MakeInfoObject("Extra Points\n+5% score");
                }
                else if (randomBoom <  12) {            //Mega Star Bubble
                    infoGenerator.MakeInfoObject("Mega Star Bubble");
                    Instantiate(megaBubble, new Vector3(Random.Range(-2.5f, 2.5f), -5, 0), Quaternion.identity, transform);
                }
                else if (randomBoom <  27) {            //Salute
                    infoGenerator.MakeInfoObject("Salute");
                    for (int i = 0; i < 3; i++) 
                    {
                        spawner.MissileSpawn();
                    }
                }
                else {                            //The Furious Sheep
                    infoGenerator.MakeInfoObject("The Furious Sheep");
                    Instantiate(FuriousSheep);
                }
                break;
            case 6: //lightning
                infoGenerator.MakeInfoObject("300 score");
                score.AddToScore(300);
                break;

            case 7: //M
                spawner.MpBuble(true);
                break;
            case 8: //P
                spawner.MpBuble(false);
                break;
            case 9: //star
                score.AddToScore(10);
                infoGenerator.MakeInfoObject("+1 Star\n+10 score");
                player.GetComponent<PlayerHPSystem>().ChangeOwnedStars(1);
                break;
            case 10: //dawniej combobreaker - blackhole
                infoGenerator.MakeInfoObject("Black Hole");
                Instantiate(blackHole, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-2, 2f), 0), Quaternion.identity);
                break;

            case 12: //missile
                infoGenerator.MakeInfoObject("Missile");
                spawner.MissileSpawn();
                break;

            case 13: //UFO Attack
                infoGenerator.MakeInfoObject("UFO Attack");
                Instantiate(ufo);
                break;

            case 16:
                infoGenerator.MakeInfoObject("+10 Star");
                player.GetComponent<PlayerHPSystem>().ChangeOwnedStars(10);
                break;
            case 17:
                int randomSupport = Random.Range(1, 4);
                if(randomSupport < 2) {
                    infoGenerator.MakeInfoObject("Extra Shield");
                    player.GetComponent<PlayerHPSystem>().AddSuperShield();
                }
                else if (randomSupport < 2) {
                    score.AddToScore(200);
                    infoGenerator.MakeInfoObject("+200 score");
                }
                else {
                    infoGenerator.MakeInfoObject("+10 Star");
                    player.GetComponent<PlayerHPSystem>().ChangeOwnedStars(10);
                }
                break;
            case 18:
                infoGenerator.MakeInfoObject("Extra Shield");
                player.GetComponent<PlayerHPSystem>().AddSuperShield();
                break;

            default:
                //Debug.Log("Nieznany babelek!");
                break;
        }
    }

    public void ComboEngage(int combo) //public tylko na chwile (do wpisywania combo w grze)
  {
        // Debug.Log(combo);
        switch (combo) {
            case 123:// tylko do testowania dodaje gwiazdek na shopping trzeba usunac pozniej
                player.GetComponent<PlayerHPSystem>().ChangeOwnedStars(10000);
                break;
            case 111:
                infoGenerator.MakeInfoObject("GodMode ON");
                player.GetComponent<PlayerHPSystem>().godMode = true;
                break;
            case 112:
                infoGenerator.MakeInfoObject("GodMode OFF");
                player.GetComponent<PlayerHPSystem>().godMode = false;
                break;

            case 7: //Alternavive Obstacles //challenge
                float alterObstacleTime = Random.Range(1, 2.5f);
                SafeBigInfoGeneration("Alternavive Obstacles");
                obstacleGenerator.SetSawObstacleMode(true);
                alterObstacleTime *= 60;
                StartCoroutine(StopEffect(alterObstacleTime, 111000300));
                break;

            case 10: //Missile Ride     //challenge
                GetComponent<BubbleGeneratorScript>().SetAlternativeMode(true, false, true);
                obstacleGenerator.obstacleSpawning = false;
                spawner.SpawnObstacleDestroyingLine();
                player.GetComponent<PlayerCollision>().MissileRaidMode = true;
                missileRaidDifficulty = 0; //ilosc rakiet w pierwszej salwie
                SafeBigInfoGeneration("Missile Ride Mode\nActivated");
                StartCoroutine(MissileRaid(true));
                break;

            case 14: //Super Mystery Air Support    //challenge
                SafeBigInfoGeneration("Super Air Support\nhave rest");
                Instantiate(MysteryAirSupport, new Vector3(0, 5, 0), Quaternion.identity, transform);
                Instantiate(MysteryAirSupport, new Vector3(1.5f, 5.5f, 0), Quaternion.identity, transform);
                Instantiate(MysteryAirSupport, new Vector3(-1.5f, 5.5f, 0), Quaternion.identity, transform);
                Instantiate(MysteryAirSupport, new Vector3(3, 6, 0), Quaternion.identity, transform);
                Instantiate(MysteryAirSupport, new Vector3(-3, 6, 0), Quaternion.identity, transform);
                break;

            case 17: //Boss challenge
                SafeBigInfoGeneration("Boss Summoned");
                spawner.SpawnFisrtBoss();
                break;

            case 116:
                infoGenerator.MakeInfoObject("Destroying Obstacles & Bubbles");
                spawner.SpawnDestroyingLine();
                break;
            case 119:
                infoGenerator.MakeInfoObject("Destroying Obstacles");
                spawner.SpawnObstacleDestroyingLine();
                break;
            case 120:
                infoGenerator.MakeInfoObject("Destroying Bubbles");
                spawner.SpawnBubbleDestroyingLine();
                break;

            default:
                Debug.Log("Comboingage blad");
                break;
        }
    }

    public void SafeBigInfoGeneration(string text) {
        if (bigInfoGenerator.SetInfo(text)) {
            infoGenerator.MakeInfoObject(text);
        }
    }
    IEnumerator ChallengeSoon() {
        yield return new WaitForSeconds(3);
        Challenge();
    }

    IEnumerator StopEffect(float time, int comboIndex)
    {
        yield return new WaitForSeconds(time);
        if(comboIndex == 100200030) // stars festival
        {
            infoGenerator.MakeInfoObject("Stars Festival End");
            GetComponent<BubbleGeneratorScript>().SetAlternativeMode(false);
        }
        else if (comboIndex == 111000300) //alternatywne obstacle mode
        {
            infoGenerator.MakeInfoObject("Alternative Obstacle End");
            obstacleGenerator.SetSawObstacleMode(false);
            SafeBigInfoGeneration("challenge complited\n+10000points");
            score.AddToScore(10000);
        }
    }
    IEnumerator MissileRaid( bool firstRaid)
    {
        if( firstRaid )
        {
            yield return new WaitForSeconds(2);
            spawner.MissileRaid(1);
            StartCoroutine(MissileRaid(false));
        }
        else
        {
            yield return new WaitForSeconds(9);
            missileRaidDifficulty += 2;
            if (player.GetComponent<PlayerCollision>().MissileRaidMode && missileRaidDifficulty <= 10)
            {
                spawner.MissileRaid(missileRaidDifficulty);
                StartCoroutine(MissileRaid(false));
            }
            else if (player.GetComponent<PlayerCollision>().MissileRaidMode && missileRaidDifficulty > 10)
            {
                infoGenerator.MakeInfoObject("challenge complited +10000points");
                score.AddToScore(10000);
                obstacleGenerator.obstacleSpawning = true;
                GetComponent<BubbleGeneratorScript>().SetAlternativeMode(false);
            }
        }
    }

}
