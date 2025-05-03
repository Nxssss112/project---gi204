using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dropThroughTime = 0.5f;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        CheckCrouchAndDrop();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);


        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;


        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0.01f);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetBool("isJumping", !isGrounded);
    }

    void CheckCrouchAndDrop()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("isCrouching", true);
            StartCoroutine(DropThroughPlatform());
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isCrouching", false);
        }
    }

    System.Collections.IEnumerator DropThroughPlatform()
    {
        PlatformEffector2D[] effs = FindObjectsByType<PlatformEffector2D>(FindObjectsSortMode.None);
        foreach (PlatformEffector2D eff in effs)
        {
            Collider2D effCol = eff.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, effCol, true);
        }

        yield return new WaitForSeconds(dropThroughTime);

        foreach (PlatformEffector2D eff in effs)
        {
            Collider2D effCol = eff.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, effCol, false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pass"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pass"))
        {
            isGrounded = false;
        }
    }
}