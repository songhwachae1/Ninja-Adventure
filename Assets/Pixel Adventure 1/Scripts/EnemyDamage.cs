using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1; // Amount of damage dealt to the player

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
