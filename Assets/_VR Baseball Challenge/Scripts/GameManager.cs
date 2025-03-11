using Autohand;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MachineShootBall _machineShootBall;
    [SerializeField] private List<int> _scoreList = new List<int>();
    [SerializeField] private int _level;
    public List<int> ScoreList => _scoreList;
    public int Level => _level;
    [SerializeField] private bool _isPlaying;
    [SerializeField] private Stick _stick;

    private void Start()
    {
        _stick.SetModel(StickManager.Instance.CurrentStick);
        Play();
    }

    public void Play()
    {
        if (EnergyManager.Instance.Energy > 0)
        {
            AudioManager.Instance.BackgroundMusic.Play();
            EnergyManager.Instance.Energy--;
            _scoreList = new List<int>()
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
            UIGameplay.Instance.PlayUI();
            StartCoroutine(PlayMachineShootBall());
        }
        else
        {
            AudioManager.Instance.OverTurn.Play();
            UIGameplay.Instance.Notice("Over turn");
        }
    }

    private IEnumerator PlayMachineShootBall()
    {
        _isPlaying = true;
        _level++;
        UIGameplay.Instance.Notice($"Level {Level}");
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(UIGameplay.Instance.Count());
        yield return StartCoroutine(_machineShootBall.StartShootBall());
        _isPlaying = false;
    }

    public IEnumerator Check()
    {
        if (!_isPlaying)
        {
            yield return new WaitForSeconds(3);
            if (ScoreList[Level - 1] >= 5)
            {
                if(Level == 10)
                {
                    AudioManager.Instance.Win.Play();
                    UIGameplay.Instance.Notice("So Bad");
                    UIGameplay.Instance.ScoreText($"{GetScore()}");
                    yield return new WaitForSeconds(3);
                    EndGame();
                    yield break;
                }
                AudioManager.Instance.NextLevel.Play();
                UIGameplay.Instance.Notice("Well play");
                UIGameplay.Instance.ScoreText($"{GetScore()}");
                yield return new WaitForSeconds(3);
                UIGameplay.Instance.ScoreText("");
                yield return StartCoroutine(PlayMachineShootBall());
            }
            else
            {
                AudioManager.Instance.Lose.Play();
                UIGameplay.Instance.Notice("So Bad");
                UIGameplay.Instance.ScoreText($"{GetScore()}");
                yield return new WaitForSeconds(3);
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        AudioManager.Instance.BackgroundMusic.Stop();
        int score = ScoreList.Sum();
        UIGameplay.Instance.EndGame(score);
    }

    public void AddScore()
    {
        ScoreList[Level - 1]++;
        StartCoroutine(Check());
    }

    public int GetScore()
    {
        return ScoreList[Level - 1];
    }

    public void MissBall()
    {
        //todo: miss ball
        StartCoroutine(Check());
    }
}
