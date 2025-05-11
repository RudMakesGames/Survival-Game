using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("Gun Variables and References")]
    [SerializeField] private GameObject Rifle, Revolver, Smg;
    [SerializeField] private Transform RifleMuzzle, RevolverMuzzle, SmgMuzzle;
    [SerializeField] private float FireRate;
    [SerializeField] private Camera cam;
    private float NextFireTime = 0f;
    private float DamageAmount;
    public GameObject MuzzleFlash, HitParticles, ReloadIndicator;

    [Header("Sfx")]
    [SerializeField] private AudioClip RifleSfx, RevolverSfx, SMGSfx;

    [Header("Bullet Count and Reload")]
    public int RifleMag = 40, RevolverClip = 6, SmgMag = 30;
    private bool canShoot = true;
    public GameObject RifleAmmo, RevolverAmmo, SMGAmmo;
    public float ReloadTime;
    public bool isReloading;

    private enum GunState { None, Rifle, Revolver, Smg }
    private GunState CurrentGunState;

    [Header("Camera Shake Settings")]
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    [Header("Critical/ Hit sound")]
    [SerializeField]
    private AudioClip CriticalHitsfx;
    [SerializeField]
    private AudioClip Hitsfx;
    void Start() { SwitchState(GunState.Rifle); }

    void Update()
    {
        ReloadWeapon();
        WeaponChange();
        HandleShooting();
    }

    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchState(GunState.Rifle);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchState(GunState.Revolver);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchState(GunState.Smg);
    }

    private void SwitchState(GunState newState)
    {
        CurrentGunState = newState;
        Rifle.SetActive(newState == GunState.Rifle);
        RifleAmmo.SetActive(newState == GunState.Rifle);
        Revolver.SetActive(newState == GunState.Revolver);
        RevolverAmmo.SetActive(newState == GunState.Revolver);
        Smg.SetActive(newState == GunState.Smg);
        SMGAmmo.SetActive(newState == GunState.Smg);
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading..");
        isReloading = true;
        canShoot = false;
        ReloadIndicator.SetActive(true);
        yield return new WaitForSeconds(ReloadTime);
        if (CurrentGunState == GunState.Rifle) RifleMag = 40;
        if (CurrentGunState == GunState.Revolver) RevolverClip = 6;
        if (CurrentGunState == GunState.Smg) SmgMag = 30;
        isReloading = false;
        canShoot = true;
        ReloadIndicator.SetActive(false);
        Debug.Log("Reloaded!");
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= NextFireTime && canShoot)
        {
            NextFireTime = Time.time + FireRate;
            switch (CurrentGunState)
            {
                case GunState.Rifle:
                    FireWeapon("Rifle Bullet", RifleMuzzle, RifleSfx, 0.15f, 1.5f, 40, 0.1f);
                    break;
                case GunState.Revolver:
                    FireWeapon("Revolver Bullet", RevolverMuzzle, RevolverSfx, 0.6f, 0.75f, 100, 0.2f);
                    break;
                case GunState.Smg:
                    FireWeapon("Smg Bullet", SmgMuzzle, SMGSfx, 0.09f, 1.25f, 25, 0.05f);
                    break;
            }
        }
    }

    private void FireWeapon(string bulletType, Transform muzzle, AudioClip sound, float fireRate, float reloadTime, float damage, float shakeIntensity)
    {
        FireRate = fireRate;
        ReloadTime = reloadTime;
        DamageAmount = damage;

        
        GameObject flash = Instantiate(MuzzleFlash, muzzle.position, muzzle.rotation);
        Destroy(flash, flash.GetComponent<ParticleSystem>().main.duration);

        
        AudioManager.instance.PlaySoundFXClip(sound, transform, 1, Random.Range(0.9f, 1));

        
        if (CurrentGunState == GunState.Rifle) RifleMag--;
        if (CurrentGunState == GunState.Revolver) RevolverClip--;
        if (CurrentGunState == GunState.Smg) SmgMag--;

       
        StartCoroutine(CameraShake(shakeDuration, shakeIntensity));

        
        FireBullet(bulletType);

        
        if ((CurrentGunState == GunState.Rifle && RifleMag <= 0) ||
            (CurrentGunState == GunState.Revolver && RevolverClip <= 0) ||
            (CurrentGunState == GunState.Smg && SmgMag <= 0))
        {
            StartCoroutine(Reload());
        }
    }

    private void FireBullet(string bulletType)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                GameObject hitEffect = Instantiate(HitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffect, hitEffect.GetComponent<ParticleSystem>().main.duration);

                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();
                if (enemy != null)
                {
                    if(hit.collider.CompareTag("Head") )
                    {
                        AudioManager.instance.PlaySoundFXClip(CriticalHitsfx, transform, 1, Random.Range(0.9f, 1.1f));
                    }
                    if (hit.collider.CompareTag("Body") || hit.collider.CompareTag("Limbs"))
                    {
                        AudioManager.instance.PlaySoundFXClip(Hitsfx, transform, 1, Random.Range(0.9f, 1.1f));
                    }
                    float finalDamage = hit.collider.CompareTag("Head") ? DamageAmount * 2 :
                                        hit.collider.CompareTag("Limbs") ? DamageAmount / 2 : DamageAmount;
                    enemy.TakeDamage(finalDamage);
                    Debug.Log($"Hit {hit.collider.tag} for {finalDamage} damage");
                }
            }
        }
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPosition = cam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = originalPosition;
    }

    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (RifleMag < 40 || RevolverClip < 6 || SmgMag < 30)
                StartCoroutine(Reload());
        }
    }
}
