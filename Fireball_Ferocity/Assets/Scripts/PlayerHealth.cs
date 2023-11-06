using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public float maxHealth;
    public Image healthBar;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health < currentHealth) //health is adjusted first bc of other code dependencies
            //current health is local to here
        {
            currentHealth = health;
            anim.SetTrigger("fireball_damage");
        }
        
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);



        // Check if health is zero or less, then destroy the object.
        if (health <= 0)
        {
            anim.SetBool("fireball_death", true);
            Destroy(gameObject, 0.4f);
        }
    }
}