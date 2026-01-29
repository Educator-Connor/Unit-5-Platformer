using UnityEngine;

/// <summary>
/// The classic input system, has started to be deprecated in Unity 6, while sufficient for our purposes, professionally
/// the system was inefficient especially when trying to build to another device, such as cellphone or console.
/// </summary>
public class ClassicPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontal;
    private bool isGrounded;
    private bool facingRight = true;

    /// <summary>
    /// Initialize rigidbody from the gameobject this is assigned to.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Using the Input.GetAxisRaw, this takes in input from our A and D keys, alongside our Left and Right Arrow
    /// </summary>
    void Update()
    {
        // Get input
        horizontal = Input.GetAxisRaw("Horizontal");

        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Flip sprite
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();
    }
/// <summary>
/// Using rigibody based movement which is physics based, it makes more sense to handle this in fixed update which is
/// called every frame
/// </summary>
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
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

