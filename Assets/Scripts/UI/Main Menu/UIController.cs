using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private int indexGameScene = 0;

    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasSettings;

    // Fonction pour démarrer le jeu
    public void StartGame()
    {
        SceneManager.LoadScene(indexGameScene);
    }

    // Fonction pour montrer les settings
    public void LaunchSettings()
    {
        canvasMenu.SetActive(false);
        canvasSettings.SetActive(true);
    }

    // Fonction pour cacher les settings
    public void QuitSettings()
    {
        canvasMenu.SetActive(true);
        canvasSettings.SetActive(false);
    }

    // Fonction pour quitter l'application
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
