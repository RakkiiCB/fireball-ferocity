using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet; // Reference to the bullet prefab
    public Transform bulletPos;
    public float timer;

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            // Remove the Shoot() call from Update to avoid automatic shooting.
        }
    }

    // Method to be called when the AI initiates an attack.
    public void ShootOnAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}