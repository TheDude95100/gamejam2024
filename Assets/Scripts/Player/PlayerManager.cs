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
        PlayerInputs inputs = inputManager.Inputs;

        if (GameManager.gameState == GameState.Pause) {
            inputs = new PlayerInputs();
        }

        movementController.UpdateInputs(inputs);
        actionController.UpdateInputs(inputs);
    }

    void FixedUpdate()
    {
        if (inputManager.Inputs.Escape.FixedOnDown) {
            GameManager.ToggleGameState();
        }
    }

}
