using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunnerBullet : MonoBehaviour
{
    private GameObject player; 
    private Rigidbody2D rb;
    public float force;
    private float damage = 5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Use the rotation passed from the EnemyShooting script.
        Vector3 direction = player.transform.position - transform.position; 
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force; 

        float rot = Mathf.Atan2(-direction.y,-direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, 180 + rot); 
    }

    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Destroy(gameObject);
            //if (other.gameObject.GetComponent<PlayerTempInvincibility>().invincibilityTime < 0)
            //{
                
            //    other.gameObject.GetComponent<PlayerTempInvincibility>().invincibilityTime = 3;
            //    Debug.Log("bullet hit player");
            //    Debug.Log(other.gameObject.GetComponent<PlayerHealth>().health);
                
            //}
            
            
        }
    }
}