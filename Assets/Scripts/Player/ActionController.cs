using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private PlayerInputs inputs;

    private void Update()
    {
        if (inputs.Action1.OnDown)
        {
            Debug.Log("BasicAttack");
        }
        if (inputs.Action2.OnDown)
        {
            Debug.Log("HeavyStrike");
        }
        if (inputs.Action3.OnDown)
        {
            Debug.Log("Whirlwind");
        }
        if (inputs.Action4.OnDown)
        {
            Debug.Log("Conversion");
        }
    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}