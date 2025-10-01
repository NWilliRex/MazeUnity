using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [Header("Quantité de soin")]
    public int healAmount = 1; // Nombre de PV que la potion rend

    [Header("Audio")]
    public AudioSource pickupAudio; // Son joué lors de la prise
    public AudioClip pickupClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si le joueur touche la potion
        if (!other.CompareTag("Player")) return;

        // Cherche le script PlayerHealth sur le joueur
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount); // Applique le soin
        }

        // Jouer le son de ramassage si assigné
        if (pickupAudio != null && pickupClip != null)
        {
            pickupAudio.PlayOneShot(pickupClip);
        }

        // Détruire la potion après utilisation
        Destroy(gameObject);
    }
}
