using System;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private EnemyData enemyData;

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

    private void Start()
    {
        
    }

    private void Update()
    {
        if (targetPosition == null)
            return;

        Vector2 currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            enemyData.speed * Time.deltaTime
        );

        if (targetPosition.x > currentPosition.x)
            spriteRenderer.flipX = false;
        else if (targetPosition.x < currentPosition.x)
            spriteRenderer.flipX = true;

        animator.SetBool(IsMovingHash, (Vector2)transform.position != targetPosition);
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
