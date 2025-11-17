using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleSpawnEntry
{
    public GameObject prefab;

    [Range(0f, 10f)]
    public float weight = 1f;    // 가중치 
}

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    [Header("Obstacle Settings")]
    //public List<GameObject> obstaclePrefabs;
    public List<ObstacleSpawnEntry> obstacles;
    //public float spawnInterval = 2f;
    public float spawnZDistance = 30f;
    public float yPosition = 1f;

    [Header("World Reference")]
    public Transform playerTransform;    // 플레이어는 위치 기준만 필요
    public PlayerController playerController;
    //public float worldProgress = 0f;     // 게임 진행량 (플레이어가 안 움직여도 증가)


    private float timer = 0f;


    public float LastSpawnZ { get; private set; } = -999f;

    // Obstacle Manager 싱글턴
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    void Start()
    {
        // 시작 스폰 지점 : 플레이어 기준 앞쪽
        LastSpawnZ = playerTransform.position.z;
    }


    // Update is called once per frame
    void Update()
    {
        float playerZ = playerTransform.position.z;

        if (playerZ - LastSpawnZ >= spawnZDistance)
        {
            SpawnRandomObstacle(playerZ);
            LastSpawnZ = playerZ;
        }

        CleanupObstacles();
    }

    // 실제 이동 거리 대신 "게임 진행량"을 deltaTime 기반으로 증가
    /*void UpdateWorldProgress()
    {
        float speed;

        if (playerController != null)
        {
            speed = playerController.stats.forwardSpeed;
        }
        else
        {
            speed = 10f;
        }

        worldProgress += speed * Time.deltaTime;
    }*/

    // 가중치 기반 랜덤 선택 
    GameObject GetWeightedRandomObstacle()
    {
        float totalWeight = 0f;
        foreach (var entry in obstacles)
            totalWeight += entry.weight;

        float randomPoint = Random.Range(0, totalWeight);
        float cumulative = 0f;

        foreach (var entry in obstacles)
        {
            cumulative += entry.weight;
            if (randomPoint <= cumulative)
                return entry.prefab;
        }

        return obstacles[0].prefab;
    }

    void SpawnRandomObstacle(float playerZ)
    {
        if (obstacles.Count == 0 || playerTransform == null)
            return;

        int lane = Random.Range(-1, 2);
        float x = lane * GlobalSetting.laneOffset;
        float z = playerZ + spawnZDistance;


        GameObject selectedPrefab = GetWeightedRandomObstacle();
        GameObject obstacle = SimpleObjectPool.Instance.GetFromPool(selectedPrefab);

        obstacle.transform.position = new Vector3(x, yPosition, z);
        obstacle.transform.rotation = Quaternion.identity;
    }

    // 뒤로 지나간 장애물 회수
    void CleanupObstacles()
    {
        float cleanupZ = playerTransform.position.z - 5f;

        foreach (Transform child in SimpleObjectPool.Instance.transform)
        {
            if (!child.gameObject.activeSelf) continue;

            if (child.position.z < cleanupZ)
            {
                BaseObstacle obs = child.GetComponent<BaseObstacle>();
                obs.Deactivate();
            }
        }
    }
}
