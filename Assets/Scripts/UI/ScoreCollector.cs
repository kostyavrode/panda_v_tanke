using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using System;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestScoreText;
    private IDisposable scoreTimer;
    private bool recordRemembered;
    private int timer;
    private void OnEnable()
    {
        recordRemembered = false;
        scoreTimer?.Dispose();
        timer = 0;
        scoreTimer=Observable.Interval(System.TimeSpan.FromSeconds(1)).TakeWhile(x => TankHP.isPlayerAlive).Subscribe(x =>
          {
              timer++;
              timerText.text = timer.ToString();
          });
        TankHP.onPlayerDeath += RememberRecord;
    }
    private void OnDisable()
    {
        TankHP.onPlayerDeath -= RememberRecord;
    }
    private void RememberRecord()
    {
        if (!recordRemembered)
        {
            if (!PlayerPrefs.HasKey("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", timer);
            }
            else if (PlayerPrefs.GetInt("BestScore") < timer)
            {
                    PlayerPrefs.SetInt("BestScore", timer);
            }
            bestScoreText.text += PlayerPrefs.GetInt("BestScore").ToString();
            recordRemembered = true;
        }
    }
}
