using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim = null;
    public Transform player;

    public float detectionRange = 10f;
    public float attackRange = 1.5f;

    public int health = 100;

    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Walk toward player
        if (dist < detectionRange && dist > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        // Attack player
        if (dist <= attackRange)
        {
            agent.isStopped = true;
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;

        anim.SetTrigger("gotHit");

        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        agent.isStopped = true;
        anim.SetBool("isDead", true);

        // Disable all colliders
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        Destroy(gameObject, 30f); // remove body after 30 seconds
    }
}
