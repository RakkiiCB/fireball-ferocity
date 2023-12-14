
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player character.
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 2.0f; // Time between attacks
    public float chaseDistance = 10f; // Maximum chase distance
    public float damage = 5f; // Damage value 
    public LayerMask whatIsEnemies;

    private Animator animator;
    private bool isChasing = false;
    private float lastAttackTime = 0f;
    public Transform attackPos; 
    
    private EnemyShooting enemyShooting; // Reference to the EnemyShooting component

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Get the EnemyShooting component attached to the same GameObject.
        enemyShooting = GetComponent<EnemyShooting>();
    }

    private void Update()
    {
        // Check if the player is within the chase distance.
        if (Vector2.Distance(transform.position, player.position) <= chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                ChasePlayer();
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                }
            }
            else
            {
                // The player is out of attack range, continue chasing.
                ChasePlayer();
            }
        }
        else
        {
            // Stop chasing when the player is out of the chase distance.
            animator.SetBool("IsRunning", false);
        }
    }

    private void ChasePlayer()
    {
        Vector2 moveDirection = player.position - transform.position;

        // Calculate the distance between the enemy and the player.
        float distanceToPlayer = moveDirection.magnitude;

        // Check if the player is within the attack range.
        if (distanceToPlayer <= attackRange)
        {
            // Player is within attack range; stop moving towards them.
            animator.SetBool("IsRunning", false);
            RotateTowardsPlayer();
            return;
        }

        if (distanceToPlayer <= chaseDistance)
        {
            RotateTowardsPlayer();

            // Set the "IsRunning" parameter in the animator to true.
            animator.SetBool("IsRunning", true);

            // Move towards the player.
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Player is out of chase distance; stop chasing.
            animator.SetBool("IsRunning", false);
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            // Player is to the right of the enemy; flip to face right.
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            // Player is to the left of the enemy; flip to face left.
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    private void Attack()
    {
        // Implement your attack behavior here.
        // Play attack animations and deal damage to the player.
        animator.SetTrigger("Attack");

        // Check if the enemy has a shooting component.
        if (enemyShooting != null)
        {
            // Call the shooting function in the EnemyShooting script.
            enemyShooting.ShootOnAttack();
        }
        else
        {
            Collider2D[] targetPlayer = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < targetPlayer.Length; i++)
            {
                targetPlayer[i].GetComponent<PlayerHealth>().health -= damage;
            }
        }

        // Update the last attack time.
        lastAttackTime = Time.time;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
