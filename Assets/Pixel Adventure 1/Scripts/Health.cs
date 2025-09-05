using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health value
    public int currentHealth; // Current health value
    public GameObject healthIconPrefab; // Prefab for health icon
    public Transform healthIconParent; // Parent object to hold health icons
    public float iconSpacing = 50f; // Spacing between health icons
    public Animator animator; // Reference to the Animator component
    public float invulnerabilityDuration = 2f; // Duration of invulnerability after taking damage
    public UIManager uiManager; // Reference to the UIManager component

    private Image[] healthIcons; // Array to hold references to health icons
    private bool isInvulnerable = false; // Flag to check if player is invulnerable

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
				uiManager = FindObjectOfType<UIManager>();
        healthIcons = new Image[maxHealth]; // Initialize health icons array
        // Instantiate health icons and place them at different x positions
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject icon = Instantiate(healthIconPrefab, healthIconParent);
            icon.transform.localPosition = new Vector3(i * iconSpacing, 0, 0); // Set x position based on spacing
            healthIcons[i] = icon.GetComponent<Image>();
        }
        UpdateHealthDisplay(); // Update health display at the start
    }

    // Function to decrease health
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage; // Decrease current health by damage amount
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
            UpdateHealthDisplay(); // Update health display after taking damage
            animator.SetTrigger("Hit"); // Trigger 'Hit' animation
            if (currentHealth <= 0)
            {
                Die(); // Call Die function if health reaches 0
            }
            else
            {
                StartCoroutine(InvulnerabilityCoroutine()); // Start invulnerability coroutine
            }
        }
    }

    // Coroutine to handle invulnerability duration
    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    // Function to update health display
    void UpdateHealthDisplay()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].enabled = true; // Enable health icon if within current health
            }
            else
            {
                healthIcons[i].enabled = false; // Disable health icon if beyond current health
            }
        }
    }

    // Function to handle character death
    void Die()
    {
        uiManager.DisplayGameOver(); // Call DisplayGameOver function from UIManager
        Debug.Log("Character is dead.");
    }

}


