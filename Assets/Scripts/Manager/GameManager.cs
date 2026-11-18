using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; private set; }

    public event Action<int> OnScoreChanged;
    public event Action<bool> OnPauseStateChanged;

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
}
