using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    private int playerScore = 0;

    public delegate void gameEvent();

    // use event keyword if only the owner of the delegate is allowed to cast
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnScoreIncrease;
    public static event gameEvent OnRestartGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        OnScoreIncrease();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    public void restartGame()
    {
        // this did not work out, too many things to reset :")
        OnRestartGame();
    }
}
