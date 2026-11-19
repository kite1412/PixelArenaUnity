using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public int CurrentHealth { get; private set; }

    public event Action<int> OnHealthChanged;

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} has been destroyed.");
            Destroy(gameObject);
        }
        else
            OnHealthChanged?.Invoke(CurrentHealth);
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
}
