using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MovementController movementController;
    [SerializeField] private ObjectColliderDetector colliderDetector;
    [SerializeField] private ActionController actionController;

    [SerializeField] private GameObject pauseMenu;


    void OnEnable()
    {
        inputManager = GetComponent<InputManager>();
        movementController = GetComponent<MovementController>();
        colliderDetector = GetComponent<ObjectColliderDetector>();
        actionController = GetComponent<ActionController>();
    }

    void Update()
    {
        PlayerInputs inputs = new PlayerInputs();

        if (inputManager.Inputs.Escape.OnDown) {
            TogglePause();
        }

        if (GameManager.gameState != GameState.Pause) {
            inputs = inputManager.Inputs;
        }

        pauseMenu.SetActive(GameManager.gameState == GameState.Pause);

        movementController.UpdateInputs(inputs);
        actionController.UpdateInputs(inputs);
    }

    public void TogglePause(){
        GameManager.ToggleGameState();
    }

    void FixedUpdate()
    {
    }

}
