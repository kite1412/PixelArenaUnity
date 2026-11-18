using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image healthBarFill;

    void Start()
    {
        scoreText.text = "Score: 0";
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnPauseStateChanged += TogglePauseMenu;
        GameManager.Instance.OnPlayerHealthChanged += OnPlayerHealthChanged;
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
}
