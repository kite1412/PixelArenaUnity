
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        movement = Vector2.zero;

        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                movement.x -= 1f;

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                movement.x += 1f;

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                movement.y += 1f;

            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                movement.y -= 1f;
        }
        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true;

        movement = movement.normalized;

        animator.SetBool(IsMovingHash, movement.sqrMagnitude > 0f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
