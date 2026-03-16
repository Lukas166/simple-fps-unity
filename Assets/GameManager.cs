using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stageText;

    [Header("Spawner")]
    public GameObject targetPrefab;
    public Transform spawnCenter;
    public float spawnRadius = 15f;
    public float maxSpawnHeight = 15f;

    [Header("Scale")]
    public float maxScale = 5f;

    private int currentScore = 0;
    private int currentStage = 1;

    private int targetsToSpawn = 3;
    private int targetsAlive = 0;

    private int highScore = 0;
    private int highestStage = 1;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        LoadData();
        StartStage();
    }

    void StartStage()
    {
        targetsAlive = targetsToSpawn;
        UpdateUI();
        SpawnTargets();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < targetsToSpawn; i++)
        {
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);
            float randomY = Random.Range(0f, maxSpawnHeight);

            Vector3 spawnPosition = new Vector3(
                spawnCenter.position.x + randomX,
                spawnCenter.position.y + randomY,
                spawnCenter.position.z + randomZ
            );

            GameObject target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

            float bigChance = Mathf.Clamp(0.6f - (currentStage * 0.05f), 0.1f, 0.6f);

            if (Random.value < bigChance)
            {
                float randomScale = Random.Range(2f, maxScale);
                target.transform.localScale = Vector3.one * randomScale;
            }
            else
            {
                target.transform.localScale = Vector3.one;
            }
        }
    }

    public void TargetDestroyed()
    {
        currentScore += 10;
        targetsAlive--;

        UpdateUI();

        if (targetsAlive <= 0)
        {
            NextStage();
        }
    }

    void NextStage()
    {
        currentStage++;
        targetsToSpawn += 2;

        SaveData();
        StartStage();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore + "\nHigh Score: " + highScore;
        stageText.text = "Stage: " + currentStage + "\nHighest: " + highestStage;
    }

    void SaveData()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("SavedHighScore", highScore);
        }

        if (currentStage > highestStage)
        {
            highestStage = currentStage;
            PlayerPrefs.SetInt("SavedHighStage", highestStage);
        }

        PlayerPrefs.Save();
    }

    void LoadData()
    {
        highScore = PlayerPrefs.GetInt("SavedHighScore", 0);
        highestStage = PlayerPrefs.GetInt("SavedHighStage", 1);
    }
}