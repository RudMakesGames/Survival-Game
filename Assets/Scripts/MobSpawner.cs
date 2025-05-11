using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
   
    public GameObject Enemy;
    private float TimeToSpawn;
    [Range(1,20)]
    public float SpawnInterval = 3; 
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnInterval);
        while(true)
        {
            if (ObjectiveManager.instance!= null && ObjectiveManager.instance.isRunning)
            {
                Instantiate(Enemy, transform.position, Quaternion.identity);   
                yield return new WaitForSeconds(SpawnInterval);
            }
        }
       
    }
}
