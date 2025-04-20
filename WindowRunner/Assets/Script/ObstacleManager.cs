using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    [Header("Obstacle Settings")]
    public List<GameObject> obstaclePrefabs;
    public Transform playerTransform;
    public float spawnInterval = 2f;
    public float spawnZDistance = 30f;
    public float yPosition = 1f;

    private float timer = 0f;

    
    // Obstacle Manager ╫л╠шео
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomObstacle();
            timer = 0f;
        }

    }

    void SpawnRandomObstacle()
    {
        if (obstaclePrefabs.Count == 0 || playerTransform == null)
            return;

        int lane = Random.Range(-1, 2); // -1, 0, 1
        float x = lane * GlobalSetting.laneOffset;
        float z = playerTransform.position.z + spawnZDistance;

        int prefabIndex = Random.Range(0, obstaclePrefabs.Count);
        GameObject selectedPrefab = obstaclePrefabs[prefabIndex];

        Instantiate(selectedPrefab, new Vector3(x, yPosition, z), Quaternion.identity);
    }

}
