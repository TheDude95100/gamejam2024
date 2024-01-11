using UnityEngine;

public class ActionController : MonoBehaviour
{
    private PlayerInputs inputs;
    [SerializeField] private AudioSource whirlwindSound;

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
            whirlwindSound.Play();
            Debug.Log("Whirlwind");
        }
        if (inputs.Action4.OnDown)
        {
            Debug.Log("Conversion");
        }
        if (inputs.Action3.OnUp)
        {   
            whirlwindSound.Stop();
            Debug.Log("Whirlwind");
        }
    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}