using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private static SceneManagement _inst = null;
    public static SceneManagement Inst {
        get {
            if (_inst == null) {
                _inst = new GameObject("SceneManagement").AddComponent<SceneManagement>();
            }
            return _inst;
        }
    }

    private void OnEnable() {
        DontDestroyOnLoad(this);
    }
    private void FixedUpdate() {

    }
    private void Update() {

    }
    public void Scene_Menu() {
        SceneManager.LoadScene("MenuPricipal");
    }
    public void Scene_Cinematic() {
        SceneManager.LoadScene("Cinematic");

    }
    public void Scene_Level1() {
        SceneManager.LoadScene("Level1");

    }
}
