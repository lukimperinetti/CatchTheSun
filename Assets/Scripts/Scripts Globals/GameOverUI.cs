using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text distanceTFD;
    [SerializeField] private TMP_Text levelTFD;

    private string dataFilePath;

    void Start()
    {
        dataFilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        LoadAndDisplayData();
    }

    private void LoadAndDisplayData()
    {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);

            distanceTFD.text = $"Distance: {data.distance:F2}m";
            levelTFD.text = "Level: " + data.level.ToString();
        }
        else
        {
            Debug.LogError("Data file not found!");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
