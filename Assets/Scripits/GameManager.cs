using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action onGameStarted;
    private bool isGameStarted;
    private float currentTimeScale;
    [SerializeField] private GameObject[] objectsToActivate;
    private int score;
    private int money;

    private void Awake()
    {
        instance = this;
        currentTimeScale = 1;
        TankHP.onPlayerDeath += EndGame;
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.Save();
        }
        
    }
    private void OnDisable()
    {
        TankHP.onPlayerDeath -= EndGame;
    }
    private void Start()
    {
        UIManager.instance.ShowMoney(money.ToString());
    }
    private void Update()
    {
        if (isGameStarted)
        {
            UIManager.instance.ShowScore(score.ToString());
        }
    }
    public void IncreaseScore()
    {
        score++;
    }
    public void StartGame()
    {
        isGameStarted = true;
        foreach(GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
        onGameStarted?.Invoke();
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        isGameStarted = true;
        Time.timeScale = currentTimeScale;
    }
    public void EndGame()
    {
        if (isGameStarted)
        {
            Debug.Log("ENDGAME");
            isGameStarted = false;
            CheckBestScore();
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + score);
            PlayerPrefs.Save();
            UIManager.instance.EndGame();
        }
    }
    private void CheckBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int tempBestScore = PlayerPrefs.GetInt("BestScore");
            if (tempBestScore > score)
            {
                UIManager.instance.ShowBestScore(tempBestScore.ToString());
            }
            else
            {
                UIManager.instance.ShowBestScore(score.ToString());
                PlayerPrefs.SetInt("BestScore", score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            UIManager.instance.ShowBestScore(score.ToString());
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
