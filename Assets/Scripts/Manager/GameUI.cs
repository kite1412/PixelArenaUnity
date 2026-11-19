using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    void Start()
    {
        scoreText.text = "Score: 0";
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnPauseStateChanged += TogglePauseMenu;
        GameManager.Instance.OnPlayerHealthChanged += OnPlayerHealthChanged;
        GameManager.Instance.OnGameOver += ShowGameOverMenu;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void TogglePauseMenu(bool isPaused)
    {
        pauseMenu.SetActive(isPaused);
    }

    public void OnPlayerHealthChanged(int health)
    {
        healthBarFill.rectTransform.offsetMax = 
            new Vector2(-Mathf.Clamp(100 - health, 1, 100), healthBarFill.rectTransform.offsetMax.y);
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.transform.Find("FinalScore").GetComponent<TMP_Text>().text = 
            "Final Score: " + GameManager.Instance.Score.ToString();
        gameOverMenu.SetActive(true);
    }
}
