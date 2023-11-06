using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float timeBetweenAtack;
    [SerializeField] public float startTimeBetweenAttack;
    
    [SerializeField] public Transform attackPos;
    public Animator camAnim;
    public Animator playerAnim;
    [SerializeField] public float attackRange;
    [SerializeField] public LayerMask whatIsEnemies;
    [SerializeField] public int damage;

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAtack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }


            timeBetweenAtack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAtack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
