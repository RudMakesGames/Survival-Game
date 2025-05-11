using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public Transform player;
    private GameObject PlayerObj;
    public NavMeshAgent agent;
    public Animator anim;

    public float AttackRange = 3f;
    public Transform AttackPoint;
    public float SphereRange = 5f;
    public int Damage = 10;
    public float AttackCooldown = 1f;
    public float NextAttackTime;
    public float RotationSpeed = 5f;
    public GameObject Projectile;
    [Header("Type of Enemy")]
    public bool IsAShootingEnemy;

    [Header("Flying Settings")]
    public bool IsFlyingEnemy = false;
    public float FlyingHeight = 5f; 
    public float HoverSmoothness = 2f;
    public LayerMask Hitable;
    private void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        player =  PlayerObj.transform;
        anim.SetBool("isAttacking", false);
    }
    private void Update()
    {
        if (!agent.isOnNavMesh)
        {
            Destroy(gameObject);
            return;
        }
        agent.SetDestination(player.position);
        if (IsFlyingEnemy)
        {
            MaintainFlyingHeight();
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance <= AttackRange )
        {
            Attack();
        }
        else
        {
            ContinueChasing();
        }
        FacePlayer();
    }

    private void MaintainFlyingHeight()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = Mathf.Lerp(transform.position.y, player.position.y + FlyingHeight, Time.deltaTime * HoverSmoothness);
        transform.position = targetPosition;
    }

    private void FacePlayer()
    {
        if(player!= null)
        {
            Vector3 Direction = (player.position - transform.position).normalized;
            Direction.y = 0;    
            Quaternion LookRot = Quaternion.LookRotation(Direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, LookRot, RotationSpeed * Time.deltaTime);
        }
    }

    private void ContinueChasing()
    {
        anim.SetBool("isAttacking",false);
        agent.isStopped = false;
    }

    private void Attack()
    {
        if(Time.time >= NextAttackTime)
        {
            if(IsAShootingEnemy)
            {
                anim.SetBool("isAttacking", true);
                agent.isStopped = true;
                GameObject bulletPrefab = Instantiate(Projectile, AttackPoint.position, Projectile.transform.rotation);
                Vector3 Dir = (player.position - transform.position).normalized;
               // Dir.x = UnityEngine.Random.Range(-0.05F, 0.05F);
                Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
                rb.velocity = Dir * bulletPrefab.GetComponent<BulletMovement>().BulletSpeed;
                NextAttackTime = Time.time + AttackCooldown;
            }
            else
            { 
                anim.SetBool("isAttacking", true);
                agent.isStopped = true;
                RaycastHit[] hits = Physics.SphereCastAll(AttackPoint.position, SphereRange, AttackPoint.forward, AttackRange,Hitable);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider != null)

                    {

                        Debug.Log("Triggered Tag: " + hit.collider.tag);
                        if (hit.collider.tag == "Player")
                        {
                            
                            PlayerHealth playerHealth = hit.collider.GetComponentInParent<PlayerHealth>();

                            if (playerHealth != null)
                            {
                                playerHealth.TakeDamage(Damage);
                            }
                        }
                    }

                }

                NextAttackTime = Time.time + AttackCooldown;
            }
            
        }
       
        
    }
    private void OnDrawGizmosSelected()
    {
        // Visualize the raycast in the scene view
        if (AttackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AttackPoint.position, SphereRange);
        }
    }
}
