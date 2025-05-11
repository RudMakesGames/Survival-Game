using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour,IDamagable
{
    [SerializeField]
    float MaxHealth;
    public Slider Healthbar;
    public Animator anim;
    float CurrentHealth;
    bool isDead = false;
    private GameObject Player;
    private PlayerHealth health;
    [SerializeField] private float HP = 20f;
    [SerializeField]
    private AudioClip ExplosionSfx;
    [SerializeField]
    private GameObject ExplosionPfx;
    public void HandleDeath()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        health?.RestoreHealth(HP);
        GameObject Particles = Instantiate(ExplosionPfx,transform.position, Quaternion.identity);
        float Duration = Particles.GetComponent <ParticleSystem>().main.duration;
        AudioManager.instance.PlaySoundFXClip(ExplosionSfx, transform, 1, Random.Range(0.9f, 1));
        if(ObjectiveManager.instance.KillGameMode)
        {
            KillCountManager.instance.KillCount++;
            Debug.Log("Added 1 point!");
        }
        Destroy(Particles, Duration);
        Vector3 DeathPos = transform.position;
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);  
        float clipLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Destroy(gameObject, clipLength);
    }

    public void RestoreHealth(float RestorePoints)
    {
       CurrentHealth += RestorePoints;
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                HandleDeath();
            }
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player");
        health = Player.GetComponentInParent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Healthbar != null) 
        Healthbar.value = CurrentHealth;
    }
  
}
