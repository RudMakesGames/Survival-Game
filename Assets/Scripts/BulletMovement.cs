using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float BulletSpeed = 10f;
    float Timer;
    public int Damage = 15;
    private void Update()
    {
        

        Timer += Time.deltaTime;
        if(Timer >= 3)
        {
            Destroy(gameObject);    
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}

