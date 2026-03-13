using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] bool isMainMenu;

    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;

    LevelGenerator levelGenerator;
    ScoreManager scoreManager;

    [SerializeField] float[] lanes = { -2.5f, 0, 2.5f };
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    private void Start()
    {
        if (isMainMenu) return;

        SpawnFence();
        SpawnApple();
        SpawnCoin();
    }

    public void Init(LevelGenerator lG, ScoreManager sM)
    {
        if (isMainMenu) return;

        levelGenerator = lG;
        scoreManager = sM;
    }

    void SpawnFence()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);
        for (int i = 0; i < fencesToSpawn; i++)
        {
            int selectedLane = LaneSelection();
            Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, transform);
        }
    }

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = LaneSelection();
        Vector3 spawnPos = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);

        Apple apple = Instantiate(applePrefab, spawnPos, Quaternion.identity, transform).GetComponent<Apple>();
        apple.Init(levelGenerator);
    }

    private int LaneSelection()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = LaneSelection();
        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);

            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }
    }
}