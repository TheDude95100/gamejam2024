using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    public float trigger_distance = 1;

    Transform player;
    DialogueTrigger dialogueTrigger;
    MovementModule movementModule;
    bool dialogueTriggered = false;

    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        dialogueTrigger = GetComponent<DialogueTrigger>();
        movementModule = GetComponent<MovementModule>();
        movementModule.SetLock(true);
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (!dialogueTriggered)
        {
            Debug.Log("Triggering dialogue");

            dialogueBox.SetActive(true);
            dialogueTrigger.TriggerDialogue();
            dialogueTriggered = true;
            player.GetComponent<MovementController>().DisabledControls = true;
            player.GetComponent<MovementController>().ResetAnimatorMovement();
            player.GetComponent<ActionController>().disabledClips = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, trigger_distance);
    }
}
