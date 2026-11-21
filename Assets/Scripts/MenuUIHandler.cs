using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI bestTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestScore.text = GameManager.Instance.BestScore;

        bestTime.text = GameManager.Instance.BestTime;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif

        Application.Quit();
    }
}
