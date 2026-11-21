using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int bestScore;
    public float bestTime;

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public float bestTime;
    }

    string path;

    void Awake()
    {
        // If already an instance of game object (game manager) exists in DontDestroyOnLoad(), then destroy it
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Make an instance of this gameobject
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Save file in this path
        path = Application.persistentDataPath + "/savefile.json";
        Load();
    }

    public string BestScore => bestScore == 0 ? "Best Score: 0" : $"Best Score: {bestScore}";
    public string BestTime => bestTime == 0 ? "Best Time: 0" : $"Best Time: {bestTime:F2}s";

    public void TrySetHighScore(int score, float time)
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestTime = time;
            Save();
        }
    }

    void Save()
    {
        // Store the data
        SaveData data = new SaveData {bestScore = bestScore, bestTime = bestTime};

        // Write it in the JSON
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    void Load()
    {
        // If file exists, load it
        if (File.Exists(path))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            bestScore = data.bestScore;
            bestTime = data.bestTime;
        }
    }
}
