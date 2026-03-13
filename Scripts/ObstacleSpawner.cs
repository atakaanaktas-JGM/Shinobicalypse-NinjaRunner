using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obsSpawnTime = 1f;
    [SerializeField] float minobsSpawnTime = 0.4f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;


   

    public void DecreaseObstacleSpawnTime(float amount)
    {
        obsSpawnTime -= amount;

        if (obsSpawnTime < minobsSpawnTime)
        {
            obsSpawnTime = minobsSpawnTime;
        }
    }


    IEnumerator SpawnObjects()
    {

        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];
            Vector3 spawnPos = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            Instantiate(obstaclePrefab, spawnPos, Random.rotation,obstacleParent);
            yield return new WaitForSeconds(obsSpawnTime);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
