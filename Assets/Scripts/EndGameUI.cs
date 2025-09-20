using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelVictory;   // Panel Victoire
    public GameObject panelGameOver;  // Panel Game Over

    [Header("Audio (optionnel)")]
    public AudioSource audioSource;
    public AudioClip victoryClip;
    public AudioClip gameOverClip;

    void Start()
    {
        // Désactive les panels au départ
        panelVictory.SetActive(false);
        panelGameOver.SetActive(false);
    }

    // Appelé quand le joueur gagne
    public void ShowVictory()
    {
        panelVictory.SetActive(true);
        panelGameOver.SetActive(false);

        if (audioSource != null && victoryClip != null)
            audioSource.PlayOneShot(victoryClip);
    }

    // Appelé quand le joueur meurt
    public void ShowGameOver()
    {
        panelGameOver.SetActive(true);
        panelVictory.SetActive(false);

        if (audioSource != null && gameOverClip != null)
            audioSource.PlayOneShot(gameOverClip);
    }

    // Appelé par le bouton Replay
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
