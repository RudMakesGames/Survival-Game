using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    [Header("Enemy Spawner Reference")]
    [SerializeField]
    private GameObject Spawner;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Spawner.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited by: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Spawner.SetActive(false);
        }
    }
}
