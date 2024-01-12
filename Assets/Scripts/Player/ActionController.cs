using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private AudioSource basicAttackSound;
    [SerializeField] private AudioSource heavyAttackSound;
    [SerializeField] private AudioSource whirlwindSound;
    [SerializeField] private AudioSource conversionSound;
    [SerializeField] private Animator animator;

    private PlayerInputs inputs;

    private void Update()
    {
        if (inputs.Action1.OnDown)
        {
            basicAttackSound.Play();
            animator.SetTrigger("doBasicAttack");
        }
        if (inputs.Action2.OnDown)
        {
            //heavyAttackSound.Play();
            animator.SetTrigger("doHeavyAttack");
        }
        if (inputs.Action3.OnDown)
        {   
            whirlwindSound.Play();
            Debug.Log("Whirlwind");
        }
        if (inputs.Action4.OnDown)
        {
            //conversionSound.Play();
            animator.SetTrigger("doConversion");
        }
        if (inputs.Action3.OnUp)
        {   
            whirlwindSound.Stop();
        }

    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}