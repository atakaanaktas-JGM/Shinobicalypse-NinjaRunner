using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] bool isMainMenu;

    float checkPointTimeExtension = 5f;

    ObstacleSpawner obstacleSpawner;
    GameManager gameManager;

    const string playerString = "Player";
    float decreaseAmount = 0.2f;

    private void Start()
    {
        if (isMainMenu) return;

        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMainMenu) return;

        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTimeLeft(checkPointTimeExtension);
            obstacleSpawner.DecreaseObstacleSpawnTime(decreaseAmount);
        }
    }
}