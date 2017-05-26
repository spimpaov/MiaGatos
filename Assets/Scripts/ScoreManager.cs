using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text text;

    private int currScore = 0;
    public bool newGame;

    void Start()
    {
        if (newGame)
        {
            currScore = 0;
            PlayerPrefs.SetInt("Score", 0);
            showScore();
        } else
        {
            showScore();
        }

    }

    public void somaScore(int score)
    {
        if (score == 0) //perfect
        {
            currScore = PlayerPrefs.GetInt("Score");
            currScore += 10; //score do perfect
            PlayerPrefs.SetInt("Score", currScore);

        }
        else if (score == 1) //good
        {
            currScore = PlayerPrefs.GetInt("Score");
            currScore += 5; //score do good
            PlayerPrefs.SetInt("Score", currScore);
        }

        Debug.Log("score = " + currScore);
    }

    public void showScore()
    {
        text.GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();

    }
}
