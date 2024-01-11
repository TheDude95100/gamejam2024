using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject canvasSettings;
    [SerializeField] private GameObject canvasEaster;
    [SerializeField] private AudioSource audioSource;


    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        GameManager.Scene_FakeLoad();
    }

    // Fonction pour montrer les settings
    public void LaunchSettings()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
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

    // Fonction pour montrer l'easter
    public void LaunchEaster()
    {
        canvasMenu.SetActive(!canvasMenu.activeSelf);
        canvasEaster.SetActive(!canvasEaster.activeSelf);
    }

    // Fonction pour cacher les settings
    public void QuitSettings()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 1)
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
        GameManager.Scene_Menu();
    }

    // Fonction pour quitter l'application
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
