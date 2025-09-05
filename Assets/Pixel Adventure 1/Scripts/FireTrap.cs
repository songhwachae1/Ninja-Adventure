using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    public float activationDelay = 2f; // Time before the firetrap activates
    public float activationDuration = 3f; // Duration the firetrap stays activated
    public int damage = 1; // Amount of damage dealt to the player
    public Animator animator; // Reference to the Animator component

    private bool isActivated = false;

    void Start()
    {
        StartCoroutine(ActivateFireTrap());
    }

    IEnumerator ActivateFireTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationDelay);
            isActivated = true;
            animator.SetBool("Fire", true); // Trigger activation animation
            yield return new WaitForSeconds(activationDuration);
            isActivated = false;
            animator.SetBool("Fire", false); // End activation animation
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated && collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
