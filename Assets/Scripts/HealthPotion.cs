using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healAmount = 1; // combien de PV ça rend

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // ajoute les PV
            }

            // Disparaît après utilisation
            Destroy(gameObject);
        }
    }
}
