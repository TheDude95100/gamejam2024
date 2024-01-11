using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] float timeoutHint = 0.03f;
    [SerializeField] float lastShowTime = 0f;

    public void FixedUpdate()
    {
        if (timeoutHint + Time.time <= lastShowTime)
        {

        }
    }

    public void ShowInteractHint()
    {
        lastShowTime = Time.time;
        // ajouter shader ou ui ...
        throw new System.NotImplementedException();
    }

    private void HideInteractHint()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        // faire le truc ...
        throw new System.NotImplementedException();
    }
}
