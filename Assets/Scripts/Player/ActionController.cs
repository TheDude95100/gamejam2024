using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private AudioSource basicAttackSound;
    [SerializeField] private AudioSource heavyAttackSound;
    [SerializeField] private AudioSource whirlwindSound;
    [SerializeField] private AudioSource conversionSound;
    [SerializeField] private Animator animator;

    private PlayerInputs inputs;

    private enum ClipAnims
    {
        atk_combo_anim,
        convert_anim,
        slam_anim,
        idle_anim
    }

    private float comboAttackStart = 0f;
    [SerializeField] private float comboAttackCompleteStep1 = 0f;
    [SerializeField] private float comboAttackCompleteStep2 = 0f;
    [SerializeField] private float comboAttackCompleteStep3 = 0f;

    private void Update()
    {
        string currentAnimName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        ClipAnims activeAnim;
        switch (currentAnimName)
        {
            case "atk_combo_anim":
                activeAnim = ClipAnims.atk_combo_anim;
                break;
            case "convert_anim":
                activeAnim = ClipAnims.convert_anim;
                break;
            case "slam_anim":
                activeAnim = ClipAnims.slam_anim;
                break;
            case "idle_anim":
                activeAnim = ClipAnims.idle_anim;
                break;
            default:
                activeAnim = ClipAnims.idle_anim;
                break;
        }

        if (inputs.Action1.OnDown)
        {
            basicAttackSound.Play();
            animator.SetBool("doComboAttack", true);
        }
        if (!inputs.Action1.Live && activeAnim == ClipAnims.atk_combo_anim)
        {
            basicAttackSound.Stop();
            animator.SetBool("doComboAttack", false);
        }
        if (inputs.Action2.OnDown)
        {
            heavyAttackSound.Play();
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

    public void CheckTrigger()
    {

    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}