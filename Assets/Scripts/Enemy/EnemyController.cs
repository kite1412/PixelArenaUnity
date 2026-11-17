using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    [SerializeField] private EnemyData enemyData;

    private AttackController attackController;
    private Animator animator;
    public Vector2 targetPosition;
    public Vector2 movement;

    private void Awake()
    {
        attackController = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (targetPosition == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            enemyData.speed * Time.deltaTime
        );

        movement = targetPosition - (Vector2)transform.position;
        animator.SetBool(IsMovingHash, movement.sqrMagnitude > 0f);
    }
}
