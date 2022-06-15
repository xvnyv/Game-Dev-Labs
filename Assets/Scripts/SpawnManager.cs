using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    Array values = Enum.GetValues(typeof(ObjectType));
    System.Random random = new System.Random();
    void Awake()
    {
        initSpawning();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnScoreIncrease += spawnRandomEnemy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnRandomEnemy()
    {
        StartCoroutine(delayedSpawnEnemy());
    }

    public void initSpawning()
    {
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.goombaEnemy);
    }

    void spawnFromPooler(ObjectType i)
    {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(UnityEngine.Random.Range(3, 8), gameConstants.groundSurface, 0);
            item.SetActive(true);
            Debug.Log("Spawning " + i);

        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    IEnumerator delayedSpawnEnemy()
    {
        yield return new WaitForSecondsRealtime(1);
        ObjectType enemyType = (ObjectType)values.GetValue(random.Next(values.Length));
        spawnFromPooler(enemyType);
    }
}
