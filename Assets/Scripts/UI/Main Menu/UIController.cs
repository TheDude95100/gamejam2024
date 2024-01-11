using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private int indexGameScene = 0;
    private int indexMenuScene = 1;

    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject canvasSettings;

    // Fonction pour démarrer le jeu
    public void StartGame()
    {
        SceneManager.LoadScene(indexGameScene);
    }

    // Fonction pour montrer les settings
    public void LaunchSettings()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 0)
        {
            canvasPause.SetActive(true);
            //pause game
        }
        else
        {
            canvasMenu.SetActive(false);
            canvasSettings.SetActive(true);
        }
    }

    // Fonction pour cacher les settings
    public void QuitSettings()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 0)
        {
            canvasPause.SetActive(false);
            //resume game
        }
        else
        {
            canvasMenu.SetActive(true);
            canvasSettings.SetActive(false);
        }
    }

    // Fonction pour retourner au menu
    public void LaunchMenu()
    {
        SceneManager.LoadScene(indexMenuScene);
    }

    // Fonction pour quitter l'application
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
