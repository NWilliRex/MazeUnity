using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [Header("Références")]
    public PlayerHealth player;        // Script PlayerHealth
    public Image[] hearts;             // Images des cœurs dans le canvas
    public Sprite fullHeartSprite;     // Sprite du cœur plein
    public Sprite emptyHeartSprite;    // Sprite du cœur vide

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.CurrentHealth)
                hearts[i].sprite = fullHeartSprite;  // cœur plein
            else
                hearts[i].sprite = emptyHeartSprite; // cœur vide
        }
    }
}
