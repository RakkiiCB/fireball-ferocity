using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunnerBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float damage;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerHealth>().health -= damage;
            Debug.Log("bullet hit");
            Debug.Log(other.gameObject.GetComponent<playerHealth>().health);
            Destroy(gameObject);
        }
    }
}
