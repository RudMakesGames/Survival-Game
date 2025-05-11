using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
     [Header("Health Pack Settings")]
    [SerializeField] private GameObject healthPackPrefab; // Prefab for the health pack
    [SerializeField] private float respawnTime = 5f; // Time to respawn after pickup

    private GameObject currentHealthPack; // Tracks the current active health pack

    private void Start()
    {
        // Spawn the first health pack
        SpawnHealthPack();
    }

    private void SpawnHealthPack()
    {
        // Instantiate the health pack at this spawner's position
        currentHealthPack = Instantiate(healthPackPrefab, transform.position, Quaternion.identity);

        // Parent it to the spawner for better organization in the hierarchy
        currentHealthPack.transform.SetParent(transform);

        // Attach the HealthPack script and link the spawner
        HealthPack healthPack = currentHealthPack.GetComponent<HealthPack>();
        if (healthPack != null)
        {
            healthPack.SetSpawner(this);
        }
    }

    public void OnHealthPackPickedUp()
    {
        // Start the respawn timer when the health pack is picked up
        currentHealthPack = null;
        Invoke(nameof(SpawnHealthPack), respawnTime);
    }
}
