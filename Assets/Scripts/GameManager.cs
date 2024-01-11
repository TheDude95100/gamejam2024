using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Normal,
    Pause
}

public class GameManager : MonoBehaviour
{

    private static GameManager _inst = null;
    public static GameManager Inst {
        get {
            if (_inst == null) {
                _inst = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _inst;
        }
    }

    private void OnEnable() {
        DontDestroyOnLoad(_inst.gameObject);
    }

    public static GameState gameState = GameState.Normal;
    public static void ToggleGameState(){

        if (gameState == GameState.Normal) {
            gameState = GameState.Pause;
        } else {
            gameState = GameState.Normal;
        }

    }

    public static void Scene_Menu() {
        SceneManager.LoadScene("MenuPricipal");
    }
    public static void Scene_Cinematic() {
        SceneManager.LoadScene("Cinematic");
    }
    public static void Scene_Level1() {
        SceneManager.LoadScene("Level1");
    }

}
