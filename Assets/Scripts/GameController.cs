using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController Instance;
    public TextMeshProUGUI scoreText;



    public GameObject deathPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;

    private void Awake()
    {
        Instance = this;

        deathPanel.SetActive(false);
    }
  

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public int GetScore() => score;


    public void OpenDeathPanel()
    {
        deathPanel.SetActive(true);
        finalScoreText.text = score.ToString();

        int hs = PlayerPrefs.GetInt("HighScore", 0);
        if(score > hs)
        {
            hs = score;
            PlayerPrefs.SetInt("HighScore", hs);
        }

        highScoreText.text = hs.ToString();

    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
