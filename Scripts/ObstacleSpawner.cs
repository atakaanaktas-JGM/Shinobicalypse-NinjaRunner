using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obsSpawnTime = 1f;
    [SerializeField] float minobsSpawnTime = 0.4f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;
    [SerializeField] int initialPoolSize = 5;

    private List<Queue<GameObject>> pools = new();

    void Start()
    {
        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            pools.Add(new Queue<GameObject>());
            for (int j = 0; j < initialPoolSize; j++)
                pools[i].Enqueue(CreateNew(i));
        }

        StartCoroutine(SpawnObjects());
    }

    GameObject CreateNew(int index)
    {
        GameObject obj = Instantiate(obstaclePrefabs[index], obstacleParent);
        obj.GetComponent<PooledObject>().Init(this, index);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetFromPool(int index)
    {
        if (pools[index].Count > 0)
        {
            GameObject obj = pools[index].Dequeue();
            obj.transform.SetParent(obstacleParent);
            return obj;
        }
        return CreateNew(index);
    }

    public void ReturnToPool(GameObject obj, int index)
    {
        obj.SetActive(false);
        obj.transform.SetParent(obstacleParent);
        pools[index].Enqueue(obj);
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            int index = Random.Range(0, obstaclePrefabs.Length);
            GameObject obs = GetFromPool(index);
            obs.transform.SetPositionAndRotation(
                new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z),
                Random.rotation
            );
            obs.SetActive(true);
            yield return new WaitForSeconds(obsSpawnTime);
        }
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        obsSpawnTime -= amount;
        if (obsSpawnTime < minobsSpawnTime)
            obsSpawnTime = minobsSpawnTime;
    }
}
