using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        scoreText.text = "Score: 0";
        GameManager.Instance.OnScoreChanged += UpdateScore;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
