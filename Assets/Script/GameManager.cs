using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0; 
    public int enemyScore = 0; 
    public int scoreToWin = 10;

    public Text playerScoreText;
    public Text phaseText;
    public EnemyFishController enemyFish;

    private int currentPhase = 1;
    private int maxPhases = 3;

    public GameObject victoryText;
    public GameObject defeatText;
    public GameObject retryButton;
    public GameObject pauseText;

    private bool isPaused = false;

    void Start()
    {
        victoryText.SetActive(false);
        defeatText.SetActive(false);
        retryButton.SetActive(false);
        pauseText.SetActive(false);
        UpdatePhaseText();
        UpdateScoreUI();
        UpdateEnemyCooldown();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseText.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseText.SetActive(false);
        }
    }

    void UpdateScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = $"{playerScore} - {enemyScore}";
        }
    }

    public void AddPlayerScore()
    {
        playerScore++;
        UpdateScoreUI();
        CheckWinCondition();
    }

    public void AddEnemyScore()
    {
        enemyScore++;
        UpdateScoreUI();
        CheckWinCondition();
    }

    void UpdatePhaseText()
    {
        if (phaseText != null)
        {
            phaseText.text = $"Fase {currentPhase}";
        }
    }

    void CheckWinCondition()
    {
        if (playerScore >= scoreToWin)
        {
            if (currentPhase < maxPhases)
            {
                NextPhase();
            }
            else
            {
                Victory();
            }
        }
        else if (enemyScore >= scoreToWin)
        {
            Defeat();
        }
    }

    void NextPhase()
    {
        currentPhase++;
        playerScore = 0;
        enemyScore = 0;
        UpdatePhaseText();
        UpdateScoreUI();
        UpdateEnemyCooldown();
    }

    void UpdateEnemyCooldown()
    {
        if (enemyFish != null)
        {
            switch (currentPhase)
            {
                case 1:
                    enemyFish.eatCooldown = 1.5f;
                    break;
                case 2:
                    enemyFish.eatCooldown = 1.0f;
                    break;
                case 3:
                    enemyFish.eatCooldown = 0.5f;
                    break;
                default:
                    enemyFish.eatCooldown = 0.5f;
                    break;
            }
        }
    }

    void Victory()
    {
        victoryText.SetActive(true);
        retryButton.SetActive(true);
        Time.timeScale = 0f;
    }

    void Defeat()
    {
        defeatText.SetActive(true);
        retryButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentPhase = 1;
    }
}
