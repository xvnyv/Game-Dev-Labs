using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;

    // add reference to PowerupManager
    public GameObject powerupManagerObject;
    private PowerUpManager powerUpManager;

    public GameObject spawnManagerObject;
    private SpawnManager spawnManager;

    // instantiate in start


    void Awake()
    {
        centralManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }
    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerUpManager.addPowerup(t, i, c);
    }

    public void restartGame()
    {
        // this did not work out, too many things to reset :")
        gameManager.restartGame();
    }

    public void initSpawnEnemy()
    {
        // this did not work out, too many things to reset :")
        spawnManager.initSpawning();
    }
}