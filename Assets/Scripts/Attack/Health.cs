using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} has been destroyed.");
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}
