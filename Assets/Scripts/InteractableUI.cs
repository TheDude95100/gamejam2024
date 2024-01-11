using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public void ShowInteractHint(){
        canvas.SetActive(true);
    }
    public void HideInteractHint(){
        canvas.SetActive(false);
    }
}
