using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AttackController))]
public class PlayerController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private float moveSpeed = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AttackController attackController;
    private Vector2 movement;
    private Vector2 lastMovementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackController = GetComponent<AttackController>();
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
        if (!GameManager.Instance.IsPaused)
            if (movement.x > 0)
                spriteRenderer.flipX = false;
            else if (movement.x < 0)
                spriteRenderer.flipX = true;

        movement = movement.normalized * (
            keyboard != null && keyboard.leftShiftKey.isPressed 
            ? 1.5f 
            : 1f);

        bool hasMovement = movement.sqrMagnitude > 0f;

        if (hasMovement)
            lastMovementDirection = movement.normalized;

        if (keyboard != null)
            OnAttack(keyboard.spaceKey, lastMovementDirection);

        animator.SetBool(IsMovingHash, hasMovement);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnAttack(KeyControl value, Vector2 direction)
    {
        if (value.isPressed)
            attackController.ExecuteAttack(direction);
    }
}
