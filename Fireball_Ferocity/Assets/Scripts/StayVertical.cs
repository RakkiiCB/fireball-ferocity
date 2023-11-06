using UnityEngine;

public class KeepVertical : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Ensure the Rigidbody2D stays vertical (no rotation).
        rb2d.rotation = 0.0f;
    }
}
