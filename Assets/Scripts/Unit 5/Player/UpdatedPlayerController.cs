using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The updated player controller handles more then just normal horizontal player movement
/// Added functionality for vertical movement when the player is on a ladder
/// Added knockback method
/// Added a double jump
/// </summary>
public class UpdatedPlayerController : MonoBehaviour
{
    [Header("MOVEMENT")] public float moveSpeed = 8f;
    public float jumpForce = 14f;
    public bool doubleJump = false;
    public bool onLadder = false;
    private bool knockback = false;


    [Header("GROUND CHECK")] public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    

    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;


    private PlayerInputActions input;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool facingRight = true;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        input = new PlayerInputActions();

        input.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Move.canceled += context => moveInput = Vector2.zero;
        input.Player.Jump.performed += context => jumpPressed = true;
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

          
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }

        
        // Jump
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
        if (jumpPressed && !doubleJump && !isGrounded)
        {
            doubleJump = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (isGrounded)
        {
            doubleJump = false;
        }
        
        jumpPressed = false;

        // Flip sprite
        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        if (knockback)
        {
            return;
        }
        
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (onLadder)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * moveSpeed);
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Knockback(GameObject contact, float knockbackForce)
    {
        knockback = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce((this.gameObject.transform.position - contact.gameObject.transform.position).normalized * knockbackForce, ForceMode2D.Impulse);
        this.gameObject.GetComponent<HealthHandler>().Damage();
        StartCoroutine("KnockbackDuration");
        CameraShake.Instance.Shake(.5f, 1.5f);
       
    }

    public IEnumerator KnockbackDuration()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        knockback = false;
    }

    public void SetOnLadder(bool onLadder)
    {
        this.onLadder = onLadder;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public PlayerInputActions GetInput()
    {
        return input;
    }
}
