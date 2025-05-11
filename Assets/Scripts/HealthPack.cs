using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public GameObject HealthPfx;
    private HealthSpawner spawner;
    [SerializeField]
    private AudioClip Healsfx;
    public void SetSpawner(HealthSpawner spawner)
    { 
        this.spawner = spawner;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth.Currenthealth < playerHealth.Maxhealth)
            {
                playerHealth.RestoreHealth(50);
                spawner?.OnHealthPackPickedUp();   
                GameObject healVfx = Instantiate(HealthPfx, transform.position, Quaternion.identity);
                float VfxDuration = healVfx.GetComponent<ParticleSystem>().main.duration;
                AudioManager.instance.PlaySoundFXClip(Healsfx,transform,1,Random.Range(0.9f,1.1f));
                Destroy(healVfx, VfxDuration);
                Destroy(gameObject);
            }
           
        }
    }
}
