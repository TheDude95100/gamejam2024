using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float decceleration = 7f;
    [SerializeField] private float velPower = 1.2f;
    [SerializeField] private float friction = 0.7f;

    [Header("JUMP")]
    [SerializeField] private float jumpForce = 5f;
    // reduces current y velocity by amount[0-1] (higher the CutMultiplier the less sensitive to input it becomes)
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private int jumpCoyoteFrame = 10;
    [SerializeField] private int jumpBufferFrame = 10;
    [SerializeField] private float fallGravityForce = 1.9f;


    [Header("SERIALIZE")]

    // [SerializeField] private PlayerMoveStats stats;
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float distanceAvecLeSol;

    private Rigidbody rb;
    private PlayerInputs inputs;

    private bool grounded;
    private bool facingRight;

    #region Public

    public bool DisabledControls;

    public Vector3 Speed => rb.velocity;
    public bool Falling => rb.velocity.y < 0;
    public bool Jumping => rb.velocity.y > 0;
    public bool FacingRight => facingRight;
    public bool Grounded => grounded;

    #endregion

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    int fixedUpdateCount = 0;
    private void Update()
    {
        IsGrounded();
        // Debug.Log(_grounded);
        if (inputs.Jump.OnDown)
        {
            jumpToConsume = true;
            frameJumpWasPressed = fixedUpdateCount;
        }
    }

    private void FixedUpdate()
    {
        fixedUpdateCount++;
        // Debug.Log(_frameInput.Movement2d.Live);
        if (DisabledControls) return;
        Horizontal();
        // Jump();
        GravityModifier();
    }

    public void UpdateInputs(PlayerInputs inputs){
        this.inputs = inputs;
    }

    private void IsGrounded()
    {
        bool groundedLive = Physics.Raycast(transform.position, Vector3.down, distanceAvecLeSol, groundLayerMask);
        Debug.DrawRay(transform.position, new Vector3(0,-distanceAvecLeSol,0));
        if (!groundedLive && grounded)
        {
            frameLeftGround = fixedUpdateCount;
        }
        else if (groundedLive && !grounded)
        {
            ResetJump();
        }

        grounded = groundedLive;
    }

    private float axisZ(){

        // calculate wanted direction and desired velocity
        float targetSpeed = (inputs.Movement2d.Live.y == 0 ? 0 : MathF.Sign(inputs.Movement2d.Live.y)) * moveSpeed;
        // calculate difference between current volocity and target velocity
        float vel = rb.velocity.z;
        float speedDif = targetSpeed - vel;
        // change acceleration rate depending on situations;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
        // applies acceleration to speed difference, raise to a set power so acceleration increase with higher speed
        // multiply by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        // apply the movement force
        return movement;
    }

    private float axisX()
    {
        // calculate wanted direction and desired velocity
        float targetSpeed = (inputs.Movement2d.Live.x == 0 ? 0 : MathF.Sign(inputs.Movement2d.Live.x)) * moveSpeed;
        // calculate difference between current volocity and target velocity
        float vel = rb.velocity.x;
        float speedDif = targetSpeed - vel;
        // change acceleration rate depending on situations;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : decceleration;
        // applies acceleration to speed difference, raise to a set power so acceleration increase with higher speed
        // multiply by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        // apply the movement force
        return movement;
    }

    private void Horizontal(){
        rb.AddForce(new Vector3(axisX(), 0, axisZ()));
        Debug.Log(rb.velocity);
    }

    // private void HorizontalTransform

    #region Jump

    private bool jumpToConsume;
    private int frameJumpWasPressed;
    private int frameLeftGround;

    private bool bufferedJumpUsable;
    private bool coyoteUsable;


    private bool HasBufferedJump => bufferedJumpUsable && fixedUpdateCount < frameJumpWasPressed + jumpBufferFrame;
    private bool CanUseCoyote => coyoteUsable && !grounded && fixedUpdateCount < frameLeftGround + jumpCoyoteFrame;

    private void Jump()
    {
        if (jumpToConsume || HasBufferedJump)
        {
            if (grounded || CanUseCoyote) NormalJump();
        }
        jumpToConsume = false;
    }

    private void NormalJump()
    {
        coyoteUsable = false;
        bufferedJumpUsable = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        coyoteUsable = true;
        bufferedJumpUsable = true;
    }

    private void GravityModifier()
    {
        if (!Falling) return;
        rb.AddForce(Vector3.down * fallGravityForce, ForceMode.Force);
    }
    #endregion

    public void FreezePosition(FreezePositionAxis axis)
    {
        rb.constraints = axis switch
        {
            FreezePositionAxis.X => RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX,
            FreezePositionAxis.Z => RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ,
            _ => rb.constraints
        };
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }

}

public enum FreezePositionAxis { X, Z }