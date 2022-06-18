using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 goombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
                                                                  // .. other reset values 

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for EnemyController.cs
    public float maxOffset = 5f;
    public float enemyPatroltime = 1.5f;
    public float groundDistance = -0.6f;
    public float groundSurface = -0.45f;

    // for ConsumableMushroomController.cs
    public float enlargeScale = 1.5f;

    // for testing
    public int testValue;

    // Mario basic starting values
    public int playerMaxSpeed = 50;
    public int playerMaxJumpSpeed = 17;
    public int playerDefaultForce = 150;

    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
