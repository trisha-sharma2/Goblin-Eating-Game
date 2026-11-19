using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    ObjectPooler objectPooler;

    private float xSpawnRange = 14.00f;
    private float zSpawnRange = 5.75f;
    private float yPowerupSpawn = 1.17f;
    private float yGoblinHumanSpawn = 0.5f;

    private float powerupSpawnTime = 10.0f;
    private float goblinSpawnTime = 3.0f;
    private float humanSpawnTime = 3.0f;
    private float startDelay = 1.0f;

    private string[] humanTags = {"Human 1", "Human 2"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        InvokeRepeating("SpawnGoblin", startDelay, goblinSpawnTime);
        InvokeRepeating("SpawnRandomHuman", startDelay, humanSpawnTime);
        InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);
    }

    void SpawnGoblin()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, yGoblinHumanSpawn, zSpawnRange);

        objectPooler.SpawnFromPool("Goblin", spawnPos, Quaternion.identity);
    }

    void SpawnRandomHuman()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, yGoblinHumanSpawn, zSpawnRange);

        int randomIndex = Random.Range(0, humanTags.Length);

        string selectedHuman = humanTags[randomIndex];

        objectPooler.SpawnFromPool(selectedHuman, spawnPos, Quaternion.identity);
    }

    void SpawnPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zSpawnRange, zSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, yPowerupSpawn, randomZ);

        objectPooler.SpawnFromPool("Powerup", spawnPos, Quaternion.identity);
    }
}
