using System;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float stoppingDistance = 1f;

    private AttackController attackController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float nextAttackTime = 0f;
    private Vector2 lastMovementDirection;
    public Vector2 targetPosition;
    public Action<EnemyController> OnDeath;

    void Awake()
    {
        attackController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        float ppu = spriteRenderer.sprite.pixelsPerUnit;

        boxCollider.size = new Vector2(
            enemyData.width / ppu,
            enemyData.height / ppu
        );
    }

    void Update()
    {
        if (targetPosition == null)
        return;

        Vector2 currentPosition = transform.position;

        float distance = Vector2.Distance(
            currentPosition,
            targetPosition
        );

        bool shouldMove = distance > stoppingDistance;

        if (shouldMove)
        {
            nextAttackTime = 0f;
            lastMovementDirection = Vector2.MoveTowards(
                currentPosition,
                targetPosition,
                Mathf.Min(
                    enemyData.speed * Time.deltaTime,
                    distance - stoppingDistance
                )
            );

            transform.position = lastMovementDirection;
        }
        else
        {
            if (nextAttackTime != 0f)
            {
                if (Time.time >= nextAttackTime)
                {
                    Vector2 direction = lastMovementDirection.normalized;
                    attackController.ExecuteAttack(direction);
                    nextAttackTime = Time.time + attackController.attack.cooldown;
                }
            }
            else
                nextAttackTime = Time.time + attackController.attack.cooldown;
        }

        if (targetPosition.x > currentPosition.x)
            spriteRenderer.flipX = false;
        else if (targetPosition.x < currentPosition.x)
            spriteRenderer.flipX = true;

        animator.SetBool(IsMovingHash, shouldMove);
    }

    void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
