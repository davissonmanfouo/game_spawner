using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private TextMeshProUGUI FinalScoreText;

    [SerializeField]
    private GameObject GameOverScreen;

    [SerializeField]
    private float ScorePerSecond = 10f;

    [SerializeField]
    private float DifficultyIncreasePerSecond = 0.03f;

    [SerializeField]
    private float MaxDifficulty = 3f;

    public int Score { get; private set; }
    public float Difficulty { get; private set; } = 1f;
    public bool IsGameOver { get; private set; }

    private float _scoreProgress;

    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        UpdateScoreText();

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        _scoreProgress += ScorePerSecond * Time.deltaTime;
        Score = Mathf.FloorToInt(_scoreProgress);
        Difficulty = Mathf.Min(
            Difficulty + DifficultyIncreasePerSecond * Time.deltaTime,
            MaxDifficulty);

        UpdateScoreText();
    }

    public void GameOver()
    {
        IsGameOver = true;
        SaveHighScore();

        if (FinalScoreText != null)
        {
            FinalScoreText.text = "Score final: " + Score + "\nMeilleur score: " + _highScore;
        }

        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }
    }

    private void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + Score + "\nMeilleur: " + _highScore;
        }
    }

    private void SaveHighScore()
    {
        if (Score <= _highScore)
        {
            return;
        }

        _highScore = Score;
        PlayerPrefs.SetInt(HighScoreKey, _highScore);
        PlayerPrefs.Save();
    }
}
