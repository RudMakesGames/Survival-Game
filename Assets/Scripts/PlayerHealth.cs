using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IDamagable
{
    public float Maxhealth;
    public float Currenthealth;
    public Slider healthSlider;
    public Animator DamageScreen;
    private Movement movement;
    [SerializeField]
    private AudioClip HurtSfx;
    [SerializeField]
    private float Duration = 0.25f, Intensity = 0.2f;
    [SerializeField]
    private Camera cam;
    private bool isDead;
    public void HandleDeath()
    {
        isDead = false;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
        movement.enabled = false;
        StartCoroutine(DeathSequence());
    }

    public void RestoreHealth(float RestorePoints)
    {
        if(!isDead)
        {
            if (Currenthealth < Maxhealth)
                Currenthealth += RestorePoints;
        }
       
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            Currenthealth -= damage;
            if (Currenthealth <= 0)
            {
                HandleDeath();
            }
            AudioManager.instance.PlaySoundFXClip(HurtSfx, transform, 1, Random.Range(0.9f, 1.1f));
            StartCoroutine(CameraShake(Duration, Intensity));
        }
       
    }

    private IEnumerator CameraShake(float Duration,float Magnitude)
    {
        Vector3 originalPos = cam.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < Duration)
        {
            float x = Random.Range(-1f,1) * Magnitude;
            float y = Random.Range(-1f, 1) * Magnitude;

            cam.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }
    void Start()
    {
        Currenthealth = Maxhealth;
        movement = GetComponent<Movement>();
    }

    
    void Update()
    {
        healthSlider.value = Currenthealth;
    }
    IEnumerator DeathSequence()
    {
       
        DamageScreen.SetTrigger("In");
        yield return new WaitForSeconds(2f);
        isDead = false;
        Destroy(gameObject);
        MenuManager.instance.BackToMenu();
    }
}
