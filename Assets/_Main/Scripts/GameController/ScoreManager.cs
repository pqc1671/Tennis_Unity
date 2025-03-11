using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    private int score;
    private int combo;
    [SerializeField] private AudioSource _audio;

    public int Score => score;

    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void IsMiss()
    {
        combo = 0;
    }

    public void SetScoreStartGame()
    {
        score = 0;
        UpdateScoreText();
    }
    
    public void Addx3Score()
    {
        score = score * 3;
        _audio.Play();
        if (score >= 500)
        {
            GamePlayManager.Instance.GameWin();
        }
    }
    public void AddScore(int amount)
    {
        combo += 1;
        if (combo == 4 )
        {
            score += amount * 2;
        }
        else
        {
            score += amount;
        }
        _audio.Play();
        UpdateScoreText();
    }

}
