using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public float deathAnimationDuration = 0.6f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Audio - Mort")]
    public AudioSource deathAudio;    // AudioSource pour la mort
    public AudioClip deathClip;       // Son joué lors de la mort

    [Header("Audio - Soin")]
    public AudioSource healAudio;     // AudioSource pour le soin
    public AudioClip healClip;   // Son joué lors du soin

    public float reloadDelay = 2f;
    public string sceneToLoad = "GameOver";


    void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialisation de l'AudioSource pour la mort si non assignée
        if (deathAudio == null)
        {
            deathAudio = gameObject.AddComponent<AudioSource>();
        }
        deathAudio.loop = false;
        deathAudio.playOnAwake = false;

        // Initialisation de l'AudioSource pour le soin si non assignée
        if (healAudio == null)
        {
            healAudio = gameObject.AddComponent<AudioSource>();
        }
        healAudio.loop = false;
        healAudio.playOnAwake = false;
    }

    // Méthode pour subir des dégâts
    public void TakeDamage(int dmg)
    {
        if (currentHealth <= 0) return; // déjà mort

        currentHealth -= dmg;
        Debug.Log("Player prend " + dmg + " dégâts. HP restants = " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    // Méthode pour soigner le joueur
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return; // ne soigne pas si mort

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // pas dépasser le max
        Debug.Log("Player récupère " + amount + " PV. HP = " + currentHealth);

        // Jouer le son du soin
        if (healClip != null)
        {
            healAudio.PlayOneShot(healClip);
        }
    }

    // Gestion de la mort
    void Die()
    {
        Debug.Log("Player est mort !");

        // Jouer le son de mort
        if (deathClip != null)
        {
            deathAudio.PlayOneShot(deathClip);
        }

        // Lancer l’animation de mort
        animator.SetTrigger("Dead");

        // Après la durée de l’animation, rendre invisible et recharger la scène
        Invoke(nameof(DisappearAndReload), deathAnimationDuration);


    }

    IEnumerator CoDelay(string name, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(name);
        if (reloadDelay <= 0f) SceneManager.LoadScene(sceneToLoad);
        else StartCoroutine(CoDelay(sceneToLoad, reloadDelay));
    }


    void DisappearAndReload()
    {
        spriteRenderer.enabled = false; // rend le joueur invisible
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // recharge la scène
    }


}
