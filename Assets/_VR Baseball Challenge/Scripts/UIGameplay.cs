using Oculus.Interaction;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;

    [SerializeField] private Text _notionText;
    [SerializeField] private Text _scoreText;

    private void Awake()
    {
        _homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Click.PlayScheduled(0);
            SceneManager.LoadSceneAsync("Menu");
        });
        _replayButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Click.PlayScheduled(0);
            GameManager.Instance.Play();
        });
    }

    public void Notice(string text)
    {
        _notionText.text = text;
    }

    public void ScoreText(string score)
    {
        _scoreText.text = score;
    }

    public void PlayUI()
    {
        _homeButton.gameObject.SetActive(false);
        _replayButton.gameObject.SetActive(false);
        _scoreText.text = "";
    }

    public IEnumerator Count()
    {
        int count = 3;
        while (true)
        {
            yield return new WaitForSeconds(1);
            AudioManager.Instance.Count.Play();
            if (count == 0)
            {
                _notionText.text = "Go!";
                break;
            }
            else
            {
                _notionText.text = $"{count}";
            }
            count--;
        }
        _notionText.text = "";
    }

    public void EndGame(int score)
    {
        StartCoroutine(ShowScore(score));
    }

    private IEnumerator ShowScore(int amount)
    {
        int score = 0;
        while (true)
        {
            yield return null;
            score++;
            _scoreText.text = $"Score: {score}";
            if (score == amount)
            {
                break;
            }
        }
        //completed
        if (score == 100)
        {
            _notionText.text = "Perfect";
        }
        else if (score >= 80 && score < 100)
        {
            _notionText.text = "Great";
        }
        else if (score >= 50 && score < 80)
        {
            _notionText.text = "Good";
        }
        else if (score > 0 && score < 50)
        {
            _notionText.text = "So Bad! Try again!";
        }
        if (score == 0)
        {
            _notionText.text = "How can you do that!";
        }
        _homeButton.gameObject.SetActive(true);
        _replayButton.gameObject.SetActive(true);
    }
}
