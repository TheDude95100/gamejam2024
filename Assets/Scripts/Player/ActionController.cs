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
        atk1,
        atk2,
        atk3,
        convert_anim,
        slam_anim,
        idle_anim
    }


    private void Update()
    {
        if (disabledClips) return;

        // string currentAnimName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        // switch (currentAnimName)
        // {
        //     case "atk1":
        //         activeAnim = ClipAnims.atk1;
        //         break;
        //     case "atk2":
        //         activeAnim = ClipAnims.atk2;
        //         break;
        //     case "atk3":
        //         activeAnim = ClipAnims.atk3;
        //         break;
        //     case "convert_anim":
        //         activeAnim = ClipAnims.convert_anim;
        //         break;
        //     case "slam_anim":
        //         activeAnim = ClipAnims.slam_anim;
        //         break;
        //     case "idle_anim":
        //         activeAnim = ClipAnims.idle_anim;
        //         break;
        //     default:
        //         activeAnim = ClipAnims.idle_anim;
        //         break;
        // }

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

    private bool atk1Active = false;
    private float atk1TimeStart = 0f;
    private float atk1TimeDo = 1f;
    [SerializeField] private ObjectColliderDetector atk1ColliderDetect;

    private void checkAttack1(){
        bool inputCheck = inputs.Action1.OnDown;
        if (!atk1Active && inputCheck) {
            atk1Active = true;
            atk1TimeStart = Time.time;
        }
        if (atk1Active && atk1TimeStart + atk1TimeDo <= Time.time) {
            atk1Active = false;
            performAttack1();
        }
        animator.SetBool("atk1", atk1Active);
    }
    private void performAttack1(){
        Debug.Log("performAttack1");
        foreach (GameObject potentialEnemy in atk1ColliderDetect.Objects)
        {
            if (potentialEnemy.TryGetComponent<EnemyBase>(out EnemyBase enemy)){
                Debug.Log($"performAttack1 {enemy.name}");
                enemy.TakeDamage(10);
            }

        }
    }
    private void checkAttack2(){
    }
    private void checkAttack3(){
    }

    public void CheckTrigger()
    {

    }

    public void UpdateInputs(PlayerInputs inputs)
    {
        this.inputs = inputs;
    }
}