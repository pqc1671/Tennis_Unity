using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    private float timePlay;
    [SerializeField] private TextMeshProUGUI timePlayText;

    [SerializeField] private bool isGameStart;
    [SerializeField] private bool isGameEnd;

    [SerializeField] private GameObject gameEnd, gameWin,khongduenergy;

    public bool IsGameStart => isGameStart;
    public bool IsGameEnd => isGameEnd;

    public event Action OnGameDone;
    public event Action OnGamePlay;

    [SerializeField] MachineShootBall machineShootBall;

    [SerializeField] private int lv = 0;

    public int Lv => lv;

    private void Start()
    {
        timePlay = 60;
        StartGame();
    }

    private void Update()
    {
        if (isGameEnd)
        {
            if (ScoreManager.Instance.Score >= 500)
            {
                GameWin();
            }
            else
            {
                GameEnd();
            }
        }
    }

    private void StartGame()
    {
        OnGamePlay?.Invoke();
        isGameStart = true;
        StartCoroutine(CountDownTime());
        ShootBall();
    }

    private IEnumerator CountDownTime()
    {
        while (isGameStart)
        {
            yield return new WaitForSecondsRealtime(1);
            timePlay -= 1;
            UpdateTimePlay();
            if (timePlay <= 0)
            {
                isGameStart = false;
                isGameEnd = true;
            }
        }
        yield break;
    }

    private void UpdateTimePlay()
    {
        timePlayText.text = $"Time Remining :{Mathf.FloorToInt(timePlay)}";
    }

    private void ShootBall()
    {
        StartCoroutine(machineShootBall.ShootBall());
    }

    public void GameEnd()
    {
        OnGameDone?.Invoke();
        Debug.Log("gameend");
        gameEnd.SetActive(true);
        isGameStart = false;
        isGameEnd = true;
    }
    public void GameWin()
    {
        LevelUp();
        Debug.Log("gamewin");

        OnGameDone?.Invoke();
        gameWin.SetActive(true);
        isGameStart = false;
        isGameEnd = true;
    }
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    private void LevelUp()
    {
        lv += 1;
    }

    public void NextLevel()
    {
        isGameEnd = false;
        timePlay = 60;
        StartGame();
    }
    public void Restart()
    {
        int energy = PlayerPrefs.GetInt("Energy");
        if (energy >0)
        {
            EnergyController.Instance.UseEnergy();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tennis");
        }
        else
        {
            khongduenergy.SetActive(true);
            gameEnd.SetActive(false);
        }
    }
}
