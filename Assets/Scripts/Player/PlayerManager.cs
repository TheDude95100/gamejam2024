using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MovementController movementController;

    void OnEnable(){
        inputManager = GetComponent<InputManager>();
        movementController = GetComponent<MovementController>();
    }

    void Update(){

        movementController.UpdateInputs(inputManager.Inputs);

    }
    void FixedUpdate(){}

}
