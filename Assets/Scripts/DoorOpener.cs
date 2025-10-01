using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorOpener : MonoBehaviour
{
    public GameObject door; // Assigner la porte dans l’Inspector
    private Animator doorAnimator;

    [Header("Scène de victoire")]
    public string victorySceneName = "Victory";

    [Header("Durée avant de changer de scène (en secondes)")]
    public float delayBeforeVictory = 2f; // mets la durée de ton animation

    void Start()
    {
        if (door)
            doorAnimator = door.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1) Joue l’anim de la porte
            if (doorAnimator)
                doorAnimator.SetTrigger("OpenDoor");

            // 2) Bloque les contrôles du joueur
            var playerMovement = other.GetComponent<PlayerMove>(); 
            if (playerMovement)
                playerMovement.enabled = false;

            // 3) Lance la coroutine avec délai
            StartCoroutine(LoadVictoryAfterDelay());
        }
    }

    private IEnumerator LoadVictoryAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeVictory);
        if (!string.IsNullOrEmpty(victorySceneName))
            SceneManager.LoadScene(victorySceneName);
        else
            Debug.LogError("[DoorOpener] victorySceneName est vide !");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && doorAnimator)
        {
            doorAnimator.SetTrigger("CloseDoor");
        }
    }
}
