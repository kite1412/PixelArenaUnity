using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }
    public int Score { get; private set; }

    public event Action<int> OnScoreChanged;

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
        IsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void AddScore(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }
}
