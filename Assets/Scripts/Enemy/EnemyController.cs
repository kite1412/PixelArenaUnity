using System;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float stoppingDistance = 1f;

    private AttackController attackController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Vector2 targetPosition;
    public Action<EnemyController> OnDeath;

    private void Awake()
    {
        attackController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
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
            transform.position = Vector2.MoveTowards(
                currentPosition,
                targetPosition,
                Mathf.Min(
                    enemyData.speed * Time.deltaTime,
                    distance - stoppingDistance
                )
            );

        if (targetPosition.x > currentPosition.x)
            spriteRenderer.flipX = false;
        else if (targetPosition.x < currentPosition.x)
            spriteRenderer.flipX = true;

        animator.SetBool(IsMovingHash, shouldMove);
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
