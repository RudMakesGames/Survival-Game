using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 8f;
    public float gravityMultiplier = 2f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    [Header("Audio")]
    [SerializeField] private AudioClip dashSFX;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isGrounded;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Prevent unwanted rotations
    }

    void Update()
    {
        HandleInput();
        CheckGroundStatus();
        HandleJumping();
        HandleDashing();
    }

    void FixedUpdate()
    {
        if (!isDashing)
            MoveCharacter();
    }

    private void HandleInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveInput = transform.right * moveX + transform.forward * moveZ;
    }

    private void MoveCharacter()
    {
        Vector3 velocity = moveInput * moveSpeed;
        velocity.y = rb.velocity.y;  // Preserve vertical movement (gravity & jumping)
        rb.velocity = velocity;
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            canJump = true;
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canJump = false;
        }

        if (!isGrounded)  // Apply custom gravity when in air
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
        }
    }

    private void HandleDashing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f && !isDashing)
        {
            StartCoroutine(Dash());
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;
        Vector3 dashVelocity = moveInput.normalized * dashSpeed;
        rb.velocity = new Vector3(dashVelocity.x, 0f, dashVelocity.z);

        // Play dash sound if available
        if (dashSFX != null)
        {
            AudioManager.instance.PlaySoundFXClip(dashSFX,transform,1,Random.Range(0.9f,1.1f));
        }

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
