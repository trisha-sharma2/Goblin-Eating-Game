using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    private int score = 0;
    private float time;
    public bool isGameOver;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public Button restartButton;
    public Button quitButton;

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            UpdateTime();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    private void UpdateTime()
    {
        time += Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        isGameOver = true;

        GameManager.Instance.TrySetHighScore(score, time);

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        Time.timeScale = 0;
    }
}
