using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    private bool grounded;
    private bool isFalling;
    private bool isLanding;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetTrigger("jump");
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        isFalling = rb.velocity.y < 0;

        anim.SetBool("run", horizontal != 0);
        anim.SetBool("grounded", grounded);
        anim.SetBool("falling", isFalling);

        if (isFalling && !isLanding)
        {
            anim.ResetTrigger("jump");
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

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
            isLanding = false;
        }
    }
}
