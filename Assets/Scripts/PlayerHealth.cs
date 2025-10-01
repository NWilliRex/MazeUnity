using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Santé")]
    public int maxHealth = 7; // PV max du joueur
    private int currentHealth;

    [Header("Vies globales")]
    public int maxLives = 3; // Nombre de tentatives
    private int currentLives;

    [Header("Mort & Respawn")]
    public float deathAnimationDuration = 1f;
    public Transform respawnPoint; // Dernier checkpoint activé
    public string gameOverScene = "GameOver";

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMove playerMove;

    [Header("Audio")]
    public AudioSource deathAudio;
    public AudioClip deathClip;
    public AudioSource healAudio;
    public AudioClip healClip;

    void Awake()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMove = GetComponent<PlayerMove>();

        if (deathAudio == null)
            deathAudio = gameObject.AddComponent<AudioSource>();

        if (healAudio == null)
            healAudio = gameObject.AddComponent<AudioSource>();

        deathAudio.loop = false;
        healAudio.loop = false;

        // Si aucun checkpoint, respawn = spawn initial
        if (respawnPoint == null)
        {
            GameObject startPoint = new GameObject("StartPoint");
            startPoint.transform.position = transform.position;
            respawnPoint = startPoint.transform;
        }
    }

    // Subir des dégâts
    public void TakeDamage(int dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth -= dmg;
        Debug.Log("PV actuels = " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    // Soin
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healClip != null)
            healAudio.PlayOneShot(healClip);

        Debug.Log("PV après soin = " + currentHealth);
    }

    // Mort
    void Die()
    {
        Debug.Log("Player est mort !");

        if (deathClip != null)
            deathAudio.PlayOneShot(deathClip);

        animator.SetTrigger("Dead");

        if (playerMove)
            playerMove.enabled = false;

        StartCoroutine(RespawnOrGameOver());
    }

    // Vérifie si GameOver ou respawn
    IEnumerator RespawnOrGameOver()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        currentLives--;

        if (currentLives <= 0)
        {
            Debug.Log("Plus de vies, Game Over !");
            SceneManager.LoadScene(gameOverScene);
        }
        else
        {
            Respawn();
        }
    }

    // Respawn au dernier checkpoint
    void Respawn()
    {
        Debug.Log("Respawn au checkpoint. Vies restantes = " + currentLives);

        currentHealth = maxHealth;
        transform.position = respawnPoint.position;

        spriteRenderer.enabled = true;
        if (playerMove) playerMove.enabled = true;

        animator.ResetTrigger("Dead");
    }

    // 🔹 Méthode publique pour HeartUI
   // Permet à d'autres scripts de lire la vie actuelle
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

}
