using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    public float trigger_distance = 1;

    GameObject player;
    Transform playerT;
    DialogueTrigger dialogueTrigger;
    MovementModule movementModule;
    bool dialogueTriggered = false;

    public GameObject dialogueBox;
    private DialogueManager dialogueManager;

    public Transform kronkCameraPos;
    private Transform oldCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.FindWithTag("Player");
        playerT = player.transform;
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
        if (dialogueTriggered)
        {
            if (dialogueManager.flag)
            {
                dialogueBox.SetActive(false);
                player.GetComponent<MovementController>().DisabledControls = false;
                player.GetComponent<ActionController>().disabledClips = false;
                GetComponent<ChickenScript>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<MovementModule>().SetLock(false);
                //GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().enabled = true;
                GameObject.FindWithTag("MainCamera").transform.position = oldCameraPos.position;
                GameObject.FindWithTag("MainCamera").transform.rotation = oldCameraPos.rotation;
            }
        }
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

            // Save the old camera position and rotation
            oldCameraPos = new GameObject("OldCameraPos").transform;
            oldCameraPos.position = GameObject.FindWithTag("MainCamera").transform.position;
            oldCameraPos.rotation = GameObject.FindWithTag("MainCamera").transform.rotation;

            // Set the new camera position and rotation
            GameObject.FindWithTag("MainCamera").transform.position = kronkCameraPos.position;
            GameObject.FindWithTag("MainCamera").transform.rotation = kronkCameraPos.rotation;

            player.GetComponent<MovementController>().DisabledControls = true;
            player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, trigger_distance);
    }
}
