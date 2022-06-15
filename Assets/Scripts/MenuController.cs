using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject originalGameObject;
    GameObject gameOverText;
    GameObject restartButton;
    // PlayerController playerController;
    // EnemyControllerLab1 enemyController;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPlayerDeath += ShowGameover;

        // playerController = FindObjectOfType<PlayerController>();
        // enemyController = FindObjectOfType<EnemyControllerLab1>();

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
            if (eachChild.name != "Score" && eachChild.name != "Powerups")
            {
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void RestartButtonClicked()
    {
        gameOverText.SetActive(false);
        restartButton.SetActive(false);

        // this did not work out, too many things to reset :")
        CentralManager.centralManagerInstance.restartGame();
    }

    void ShowGameover()
    {
        gameOverText.SetActive(true);
        // restartButton.SetActive(true);
    }
}
