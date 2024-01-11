using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void OnEnable() {
        DontDestroyOnLoad(this);
    }
    private void FixedUpdate() {

    }
    private void Update() {

    }
    private void Scene_Menu() {
        SceneManager.LoadScene("");
    }
    private void Scene_Cinematique() {

    }
    private void Scene_Niveau1() {

    }
}
