using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject VictoryPanel;
    public bool isPaused;
    void Update()
    {
        if (PausePanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        } else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void Pause()
    {
        isPaused = true;
        PausePanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void Resume()
    {
        isPaused = false;
        PausePanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void Victory()
    {
        VictoryPanel.SetActive(true);
    }

    public void Exit()
    {
        MySceneManager.Instance.LoadScene("MainMenuScene");
    }
}
