
using UnityEngine;
using UnityEngine.AI;
 
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float turnSpeed = 10f;
 
    private NavMeshAgent agent; // Auto creates a NavMeshAgent for me
    private Health targetHealth;
    private float nextAttackTime;
 
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange * 0.8f; // Setting stopping distance to 80% of attack range for smoother stopping
 
        if (target == null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();//Using first object to find palyer
            if (player != null) target = player.transform;
        }
 
        if (target != null) targetHealth = target.GetComponent<Health>();
    }
 
    private void Update()
    {
        if (target == null || targetHealth == null || targetHealth.Current <= 0f) // If target is dead
        {
            agent.isStopped = true;
            return;
        }
 
        float distance = Vector3.Distance(transform.position, target.position);//Get distance to target
 
        if (distance > attackRange) //Targeting outside attack range
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return;
        }
 
        agent.isStopped = true; // Stop moving when in attack range
        FacePlayer(); //Rotate to face the player
 
        if (Time.time >= nextAttackTime)
        {
            targetHealth.TakeDamage(attackDamage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
 
    private void FacePlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.01f) return;//Make sure rotation only happens with big enough intivals
 
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
}
 