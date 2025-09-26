using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMenu : MonoBehaviour
{
    [Header("Nom de la scène de jeu")]
    public string gameSceneName = "SampleScene";

    // Assignée au bouton "Jouer"
    public void OnPlay()
    {
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogError("[btnmenu] gameSceneName est vide.");
        }
    }

    // Assignée au bouton "Quitter"
    public void OnQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
