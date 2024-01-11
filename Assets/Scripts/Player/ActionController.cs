using UnityEngine;

public class ActionController : MonoBehaviour
{
    private PlayerInputs inputs;
    [SerializeField] private AudioSource basicAttackSound;
    [SerializeField] private AudioSource heavyAttackSound;
    [SerializeField] private AudioSource whirlwindSound;
    [SerializeField] private AudioSource conversionSound;

    private void Update()
    {
        if (inputs.Action1.OnDown)
        {
            basicAttackSound.Play();
            Debug.Log("BasicAttack");
        }
        if (inputs.Action2.OnDown)
        {
            heavyAttackSound.Play();
            Debug.Log("HeavyStrike");
        }
        if (inputs.Action3.OnDown)
        {   
            whirlwindSound.Play();
            Debug.Log("Whirlwind");
        }
        if (inputs.Action4.OnDown)
        {
            conversionSound.Play();
            Debug.Log("Conversion");
        }
        if (inputs.Action1.OnUp)
        {
            basicAttackSound.Stop();
        }
        if (inputs.Action2.OnUp)
        {
            heavyAttackSound.Stop();
        }
        if (inputs.Action3.OnUp)
        {   
            whirlwindSound.Stop();
        }
        if (inputs.Action4.OnUp)
        {   
            conversionSound.Stop();
        }
    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}