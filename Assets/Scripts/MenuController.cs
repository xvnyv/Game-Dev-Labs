using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject originalGameObject;
    GameObject gameOverText;
    GameObject restartButton;
    PlayerController playerController;
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemyController = FindObjectOfType<EnemyController>();

        originalGameObject = GameObject.Find("Canvas");
        gameOverText = originalGameObject.transform.Find("GameOver").gameObject;
        restartButton = originalGameObject.transform.Find("RestartButton").gameObject;

        gameOverText.SetActive(false);
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void SetGameOver()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartButtonClicked()
    {
        gameOverText.SetActive(false);
        restartButton.SetActive(false);

        playerController.RestartGame();
        enemyController.RestartGame();
    }
}
