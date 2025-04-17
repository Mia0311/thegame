using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    public bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (GameManager.isReturningFromIndoor)
        {
            transform.position = GameManager.playerPos;
            GameManager.isReturningFromIndoor = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }

        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}
