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

    private void Start() {
        DontDestroyOnLoad(_inst.gameObject);
        gameState = GameState.Normal;
    }

    public static GameState gameState = GameState.Normal;
    public static void ToggleGameState(){

        if (gameState == GameState.Normal) {
            gameState = GameState.Pause;
        } else {
            gameState = GameState.Normal;
        }
    }

    public static int nextLevel = 1;

    public static void Scene_Menu() {
        SceneManager.LoadScene("MenuPricipal");
    }
    public static void Scene_FakeLoad() {
        SceneManager.LoadScene("FakeLoading");
    }
    public static void Scene_Cinematic() {
        SceneManager.LoadScene("Cinematic");
    }
    public static void Scene_Level() {
        SceneManager.LoadScene($"Level{nextLevel}");
    }
    public static void Scene_Boss()
    {
        SceneManager.LoadScene("Boss");
    }
    public static void Scene_Mort()
    {
        SceneManager.LoadScene("Mort");
    }
}
