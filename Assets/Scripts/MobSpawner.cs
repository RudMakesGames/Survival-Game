using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public GameObject Enemy;
    public float SpawnInterval = 3f;

    [Header("Pooling Settings")]
    public int PoolSize = 10;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
        StartCoroutine(Spawn());
    }

    void InitializePool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemy = Instantiate(Enemy);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnInterval);

        while (true)
        {
            if (ObjectiveManager.instance != null && ObjectiveManager.instance.isRunning)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy;

        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
        }
        else
        {
            enemy = Instantiate(Enemy); // fallback in case pool runs out
        }

        enemy.transform.position = transform.position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
