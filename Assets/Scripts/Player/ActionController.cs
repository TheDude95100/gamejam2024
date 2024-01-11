using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private GameObject basicAttack;
    [SerializeField] private GameObject heavyStrike;
    [SerializeField] private GameObject whirlwind;

    private PlayerInputs inputs;

    private void Update()
    {
        if(inputs.Action1.OnDown)
        {

        }
    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}