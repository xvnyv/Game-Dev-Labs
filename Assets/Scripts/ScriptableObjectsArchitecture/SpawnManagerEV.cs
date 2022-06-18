using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour
{
    public GameConstants gameConstants;
    void Start()
    {
        Debug.Log("Spawnmanager start");
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.greenEnemy);

    }

    void startSpawn(Scene scene, LoadSceneMode mode)
    {
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.goombaEnemy);
    }


    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), gameConstants.groundDistance, 0);
            item.GetComponent<EnemyControllerEV>().originalX = item.transform.position.x;
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnRandomEnemy()
    {
        StartCoroutine(delayedSpawnEnemy());
    }

    IEnumerator delayedSpawnEnemy()
    {
        yield return new WaitForSecondsRealtime(1);
        ObjectType i = UnityEngine.Random.Range(0, 2) == 0 ? ObjectType.goombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);
    }
}