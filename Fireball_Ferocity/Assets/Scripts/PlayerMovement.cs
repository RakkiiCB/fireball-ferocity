using System;
using UnityEditor.Presets;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool isFalling; // Parameter to track falling
    private bool isLanding; // Parameter to track landing

    private void Awake()
    {
        // Grabs references for rigidbody and animator from game object
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        // Lock the Z-axis rotation to keep the object upright.
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();

        // Check if the player is falling
        isFalling = body.velocity.y < 0;

        // Set animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
        anim.SetBool("falling", isFalling);

        // Handle landing logic
        if (isFalling && !isLanding)
        {
            // Transition to landing state
            anim.ResetTrigger("jump"); // Reset jump trigger if it was set
            anim.SetTrigger("land");
            isLanding = true;
        }

        // Handle attacks
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))
        {
            Attack(); 
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void Attack()
    {
        //Pick Attack Animation based on if player is in the air
        if (grounded)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z))
                anim.SetTrigger("LightAttackGrounded");
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))
                anim.SetTrigger("HeavyAttackGrounded");
        }
        else
        {
            
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z))
                anim.SetTrigger("LightAttackAir");
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))
                anim.SetTrigger("HeavyAttackAir");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            isLanding = false; // Reset landing when grounded
        }
    }
}
