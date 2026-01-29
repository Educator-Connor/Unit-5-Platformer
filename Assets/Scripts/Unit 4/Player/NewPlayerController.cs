using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This Player Controller uses the modernised input system, this will allow us to functionally use keyboards,
/// and controllers for reading input events.
/// </summary>
public class NewPlayerController : MonoBehaviour
{
    [Header("MOVEMENT")] public float moveSpeed = 8f;
    public float jumpForce = 14f;
    [Header("GROUND CHECK")] public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    [SerializeField] private PlayerInputActions input;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool facingRight = true;
    private bool isGrounded;
    
    /// <summary>
    /// Here we initialize and subscribe our events, also assigning where the data from our input events will be stored
    /// with the => syntax. moveInput, and jumpPressed will hold these values from our input events.
    /// </summary>
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
    
    /// <summary>
    /// In update we are running our slower, less intensive checks, such as flipping the sprite, and jumping.
    /// We are also handlign the ground check in update using a physics collision, that will detect whether overlap
    /// with a layer is true or false.
    /// </summary>
    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
      
        // Jump
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpPressed = false;

        // Flip sprite
        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();
    }

    /// <summary>
    /// We are handling linear movement in FixedUpdate as this method derived from monobehaviour is checked and called
    /// every frame. This makes it ideal for physics which is perfect for our physics based movement using a rigidbody.
    /// </summary>
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Just visually represents the ground check by drawing a gizmo, only in editor to make visual debugging more easier. 
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
