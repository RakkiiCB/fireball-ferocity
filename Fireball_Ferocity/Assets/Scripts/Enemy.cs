using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private bool isAlive = true;
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Animator component on the same GameObject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            Debug.Log("Enemy TAKEN Damage");

            // Trigger the "TakeDamage" animation if the Animator is not null
            if (animator != null)
            {
                animator.SetTrigger("TakeDamageTrigger");
            }
        }
    }

    private void Die()
    {
        isAlive = false;
        Debug.Log("Enemy is dead");
        // Add any other logic here, such as playing death animations, scoring points, etc.

        // You might want to set an "isDead" parameter in the Animator to trigger a death animation
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

        // Destroy the GameObject after a delay or animation is finished
        Destroy(gameObject, 2.0f); // This example destroys the GameObject after 2 seconds
    }
}
