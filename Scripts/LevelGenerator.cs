using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CamerController camerController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject checkPointChunk;

    [Header("Level Settings")]
    [SerializeField] int startingChunkCount = 12;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    int spawnedChunkCount = 0;  




    float chunkLenght = 10f;
    Vector3 chunkPos;
   
    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }


    // Update is called once per frame
    void Update()
    {
        MoveChunk();
    }

    public void ChangeChunkSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);   


        if (newMoveSpeed != moveSpeed) 
        {
            moveSpeed = newMoveSpeed;
            float  newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
        Physics.gravity = new Vector3(Physics.gravity.x , Physics.gravity.y , newGravityZ);
        camerController.ChangeCameraFOV(speedAmount);
         }
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkCount; i++)
        {
            Spawnchunk();
        }
    }

    private void Spawnchunk()
    {
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        float chunkposZ = CalculateSpawnPos();
        chunkPos.z = chunkposZ;
        GameObject newChunkGo = Instantiate(chunkToSpawn, chunkPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunkGo);
        Chunk newChunk = newChunkGo.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
        spawnedChunkCount++;

    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (spawnedChunkCount % 8 == 0 && spawnedChunkCount != 0)
        {
            chunkToSpawn = checkPointChunk;

        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        }

        return chunkToSpawn;
    }

    void MoveChunk()
    {
       

        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
          
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLenght)
            {
                Spawnchunk();
                chunks.Remove(chunk);
                Destroy(chunk);
               

            }

        }
    }
    float CalculateSpawnPos()
    {
        float spawnPosZ;

        if (chunks.Count == 0)
        {
            spawnPosZ = transform.position.z;
        }
        else
        {
            spawnPosZ = chunks[chunks.Count - 1].transform.position.z + chunkLenght;
        }
        return spawnPosZ;   
    }
}
