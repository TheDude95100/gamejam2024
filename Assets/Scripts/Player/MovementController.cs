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
        // Debug.Log(inputs.Movement2d.Live);

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

    private Vector2 Move_XZ(Vector2 input){

        input.Normalize();
        Vector3 currentVelocity_XYZ = rb.velocity;

        Vector2 targetVelocity_XZ   = input * moveSpeed;
        Vector2 currentVelocity_XZ  = new Vector2(currentVelocity_XYZ.x, currentVelocity_XYZ.z);

        Vector2 velocityDiff = targetVelocity_XZ - currentVelocity_XZ;
        float accelerationRate = velocityDiff.magnitude > 0.001f ? acceleration : decceleration;

        float move_X = MathF.Pow(MathF.Abs(velocityDiff.x) * accelerationRate, velPower) * MathF.Sign(velocityDiff.x);
        float move_Z = MathF.Pow(MathF.Abs(velocityDiff.y) * accelerationRate, velPower) * MathF.Sign(velocityDiff.y);

        return new Vector2(move_X, move_Z);
    }

    private void Horizontal(){

        Vector2 movement = Move_XZ(inputs.Movement2d.Live);

        rb.AddForce(new Vector3(movement.x, 0, movement.y));

        // Debug.Log(rb.velocity);
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