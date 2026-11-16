using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private AttackData attack;
    [SerializeField] private LayerMask targetLayer;

    private float nextAttackTime;

    public void ExecuteAttack(Vector2 direction)
    {
        if (attack == null || 
            Time.time < nextAttackTime || 
            direction == Vector2.zero)
            return;

        direction.Normalize();

        Vector2 center = (Vector2)transform.position + direction * attack.range / 2f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Collider2D[] targets = Physics2D.OverlapBoxAll(
            center, 
            new Vector2(attack.range, attack.width), 
            angle, 
            targetLayer);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out Health health))
            {
                health.TakeDamage(attack.damage);
            }
        }

        if (attack.effectPrefab != null)
        {
            GameObject effect = Instantiate(
                attack.effectPrefab,
                center,
                Quaternion.Euler(0, 0, angle)
            );

            if (effect.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.flipY = direction.x < 0;
                spriteRenderer.sortingOrder = 11;
            }
        }

        nextAttackTime = Time.time + attack.cooldown;
    }
}
