using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjustable speed in the Inspector
    public float jumpForce = 10f; // Adjustable jump force in the Inspector
    public float coyoteTime = 0.2f; // Duration of coyote time
		public int extraJump = 3;
    public Animator animator; // Reference to the Animator component
    public LayerMask groundLayer; // Layer mask to specify what is considered ground
    public LayerMask wallLayer; // Layer mask to specify what is considered wall

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float moveInput;
    private float coyoteCounter;
    private int jumpCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (IsGrounded())
        {
            coyoteCounter = coyoteTime; // Reset coyote time counter when grounded
            jumpCounter = extraJump; 
        }
        else
        {
            coyoteCounter -= Time.deltaTime; // Decrease coyote time counter when not grounded
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coyoteCounter > 0f)
            {
                Jump();
            }
            else if (jumpCounter > 0)
            {
                DoubleJump();
            }
        }

        Flip();

        animator.SetBool("Grounded", IsGrounded());
        animator.SetBool("Run", moveInput != 0 && IsGrounded());
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger("Jump");
        coyoteCounter = 0f; // Reset coyote time counter after jumping
    }

    void DoubleJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetTrigger("Double Jump");
        jumpCounter--;
    }

    void WallJump()
    {
        rb.linearVelocity = new Vector2(-transform.localScale.x * moveSpeed, jumpForce);
        animator.SetTrigger("Wall Jump");
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;

        if ((moveInput > 0 && scaler.x < 0) || (moveInput < 0 && scaler.x > 0))
        {
            scaler.x *= -1;
        }

        transform.localScale = scaler;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    bool OnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return hit.collider != null;
    }
}
