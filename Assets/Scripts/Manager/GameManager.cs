using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; private set; }
    public int PlayerHealth { get; private set; } = 100;
    public bool IsPaused => Time.timeScale == 0f;

    public event Action<int> OnScoreChanged;
    public event Action<bool> OnPauseStateChanged;
    public event Action<int> OnPlayerHealthChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        OnPauseStateChanged?.Invoke(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        OnPauseStateChanged?.Invoke(false);
    }

    public void AddScore(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public void SetPlayerHealth(int health)
    {
        PlayerHealth = health;
        OnPlayerHealthChanged?.Invoke(PlayerHealth);
    }
}
