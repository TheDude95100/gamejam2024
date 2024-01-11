using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MovementController movementController;
    [SerializeField] private ActionController actionController;


    void OnEnable(){
        inputManager = GetComponent<InputManager>();
        movementController = GetComponent<MovementController>();
        actionController = GetComponent<ActionController>();
    }

    void Update(){

        movementController.UpdateInputs(inputManager.Inputs);
        actionController.UpdateInputs(inputManager.Inputs);

    }
    void FixedUpdate(){}

}
