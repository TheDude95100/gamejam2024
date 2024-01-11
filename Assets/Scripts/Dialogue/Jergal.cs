using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Jergal : MonoBehaviour
{
    private void Start()
    {
        DialogueTrigger dt = gameObject.GetComponent<DialogueTrigger>();
        dt.TriggerDialogue();
    }
}
