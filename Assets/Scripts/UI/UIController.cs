using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    private void Start()
    {
        TankHP.onPlayerDeath += ShowDeathScreen;
    }
    private void OnDisable()
    {
        TankHP.onPlayerDeath -= ShowDeathScreen;
    }
    private void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}
