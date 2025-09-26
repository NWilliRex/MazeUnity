using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    [Header("Musique de victoire")]
    public AudioSource victoryMusic;

    // Appeler cette fonction pour jouer la musique
    public void PlayVictory()
    {
        if (victoryMusic)
            victoryMusic.Play();
        else
            Debug.LogWarning("[VictoryUI] Aucune AudioSource assignée !");
    }
}
