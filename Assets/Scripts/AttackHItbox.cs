using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
    public int damage = 1;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        // Exemple simple: chercher un composant Health pour appliquer des dégâts
        var hp = other.GetComponent<PlayerHealth>(); // Remplacez parvotre script "Health"
        if (hp)
        {
            hp.TakeDamage(damage);
            Debug.Log("[AttackHitbox] Dégâts infligés: " + damage);
        }
    }

    // Méthodes à appeler depuis l'Animation Event
    public void EnableHitbox() { gameObject.SetActive(true); }
    public void DisableHitbox() { gameObject.SetActive(false); }
}
