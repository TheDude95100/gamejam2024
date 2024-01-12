using UnityEngine;
using UnityEngine.PlayerLoop;

public class ActionController : MonoBehaviour
{
    [SerializeField] private AudioSource basicAttackSound;
    [SerializeField] private AudioSource heavyAttackSound;
    [SerializeField] private AudioSource whirlwindSound;
    [SerializeField] private AudioSource conversionSound;
    [SerializeField] private Animator animator;

    private PlayerInputs inputs;
    public bool disabledClips = false;

    private enum ClipAnims
    {
        atk_combo_anim,
        convert_anim,
        slam_anim,
        idle_anim
    }

    private float attack1starttime = 0f;
    private bool attack1Active = false;
    [SerializeField] private ObjectColliderDetector attack1collider;

    private bool attack1s1tggl = false;
    [SerializeField] private float attack1s1time = 0.5f;
    private bool attack1s2tggl = false;
    [SerializeField] private float attack1s2time = 0.5f;
    private bool attack1s3tggl = false;
    [SerializeField] private float attack1s3time = 0.5f;

    private void Update()
    {
        if (disabledClips) return;

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

        checkAttack1();

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

    private void resetAttack1(){
        attack1Active = false;
        attack1s1tggl = false;
        attack1s2tggl = false;
        attack1s3tggl = false;
    }

    private void checkAttack1(){
        bool inputCheck = inputs.Action1.OnDown;

        // click while doing nothing
        Debug.Log($"{attack1s1tggl} {attack1starttime} {attack1Active}");
        if (!attack1s1tggl) {
            if (inputCheck) {
                attack1starttime = Time.time;
                attack1Active = true;
                attack1s1tggl = true;
                basicAttackSound.Play();
            }
        }

        // click while attacking
        if (attack1s1tggl && !attack1s2tggl) {
            if (inputCheck) {
                attack1s2tggl = true;
            }
            if (attack1starttime + attack1s1time <= Time.time){
                attack1s1tggl = false;
                attack1Active = false;
            }
        }

        if (attack1s2tggl && !attack1s3tggl) {
            if (inputCheck) {
                attack1s3tggl = true;
            }
            if (attack1starttime + attack1s2time <= Time.time){
                attack1s1tggl = false;
                attack1s2tggl = false;
                attack1Active = false;
            }
        }

        animator.SetBool("doComboAttack", attack1Active);

    }

    public void CheckTrigger()
    {

    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}