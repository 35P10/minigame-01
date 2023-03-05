using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIOnGame : MonoBehaviour
{
    [Header("On Game")]
    public TMP_Text onGameScoreTextbox;
    [Header("Game Over")]
    public GameObject gameOverScorePanel; 
    public TMP_Text gameOverScoreTextbox;
    public TMP_Text gameOverBestScoreTextbox;
    [Header("Pause")]
    public GameObject pauseScorePanel;
    public TMP_Text pauseScoreTextbox;

    // Start is called before the first frame update
    void Start(){
        gameOverScorePanel.SetActive(false);
        pauseScorePanel.SetActive(false);
        GameStatus.focus = 0;
    }

    // Update is called once per frame
    void Update(){
        if(GameStatus.isGameOver){
            GameOver();
        }
        if(!GameStatus.isGameOver && Input.GetKeyDown(KeyCode.Escape)){
            switch(GameStatus.focus){
                case 0:
                    PauseGame();
                    break;
                case 1:
                    ResumeGame();
                    break;
                default:
                    break;
            }
        }

        onGameScoreTextbox.text = (GameStatus.actualGameEnemiesKilled).ToString();
    }

    void GameOver(){
        Time.timeScale = 0f;
        gameOverScoreTextbox.text = (GameStatus.actualGameEnemiesKilled).ToString();
        gameOverBestScoreTextbox.text = (GameStatus.maxEnemiesKilled).ToString();
        gameOverScorePanel.SetActive(true);
    }

    void PauseGame(){
        GameStatus.focus = 1;
        Time.timeScale = 0f;
        pauseScoreTextbox.text = (GameStatus.actualGameEnemiesKilled).ToString();
        pauseScorePanel.SetActive(true);
    }

    public void ResumeGame(){
        GameStatus.focus = 0;
        Time.timeScale = 1f;
        pauseScorePanel.SetActive(false);
    }

    public void RestartGame(){
        GameStatus.RestartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
