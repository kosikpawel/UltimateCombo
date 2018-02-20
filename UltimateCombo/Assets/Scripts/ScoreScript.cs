using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public float scoreMultyplier;
    Text scoreText;
    float timer;
    float scoreMultyplerCopy;
    bool alternativeMode;
    //public static int finalScore;
    //bool endGameMod = false;
	
	void Start () {
        alternativeMode = false;
        timer = 0;
        scoreText = GetComponent<Text>();
        //PlayerHPSystem.endGame = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += (Time.deltaTime * scoreMultyplier);
        scoreText.text = timer.ToString("0");
	}

    public void AddToScore(int x)
    {
        timer += x;
        if (timer < 0) timer = 0;
    }
    public void AddPercentToScore(int percent)
    {
        timer += (percent/100) * timer;
    }

    public int StopScore()
    {
        scoreMultyplier = 0;
        int x = (int)timer;
        if (timer % 1 >= 0.5f) x++;
        return x;
    }
    public void MakeScoreMultyplierCopy()
    {
        scoreMultyplerCopy = scoreMultyplier;
    }

    public void StartAlternativeScoreMultypler(float multypler)
    {
        if (!alternativeMode)
        {
            alternativeMode = true;
            scoreMultyplier *= multypler;
        }
    }

    public void StopAlternativeScoreMultypler()
    {
        alternativeMode = false;
        scoreMultyplier = scoreMultyplerCopy;
    }


}
