using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        scoreText.text = "Score: 0";
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnPauseStateChanged += TogglePauseMenu;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void TogglePauseMenu(bool isPaused)
    {
        pauseMenu.SetActive(isPaused);
    }
}
