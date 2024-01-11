using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MovementController movementController;
    [SerializeField] private ObjectColliderDetector colliderDetector;

    void OnEnable(){
        inputManager = GetComponent<InputManager>();
        movementController = GetComponent<MovementController>();
        colliderDetector = GetComponent<ObjectColliderDetector>();
    }

    void Update(){

        movementController.UpdateInputs(inputManager.Inputs);

    }

    void FixedUpdate(){
    }

}
