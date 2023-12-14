using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public float maxHealth;
    public Image healthBar;
    private Animator anim;
    private float delayBeforeInvisible = .8f; // Set the delay time for the animation in seconds
    private float delayBeforeReset = 1.2f; // Set the delay time for scene reset in seconds
    private bool canMove = true; // Flag to control player movement

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
        if (canMove)
        {
            if (health < currentHealth)
            {
                currentHealth = health;
                anim.SetTrigger("fireball_damage");
            }

            healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

            // Check if health is zero or less
            if (health <= 0)
            {
                // Set the trigger for death animation
                anim.SetBool("fireball_death", true);

                // Lock player movement
                canMove = false;

                // Start a coroutine to handle the delay before making the player invisible
                StartCoroutine(MakePlayerInvisibleWithDelay());
            }
        }
    }

    IEnumerator MakePlayerInvisibleWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeInvisible);

        // Disable the renderer component to make the object invisible
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Start another coroutine to reset the scene after a delay
        StartCoroutine(ResetSceneAfterDelay(delayBeforeReset));
    }

    IEnumerator ResetSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
