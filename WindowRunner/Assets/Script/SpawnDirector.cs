using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDirector : MonoBehaviour
{

    [Header("Manager References")]
    public ObstacleManager obstacleManager;
    public RandomBoxManager randomBoxManager;

    [Header("Difficulty Settings")]
    public Transform playerTransform;
    public float minSpawnDistance = 12f;
    public float maxSpawnDistance = 30f;
    public float difficultyRampSpeed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        if (obstacleManager == null || randomBoxManager == null)
        {
            Debug.LogWarning("SpawnDirector: Manager references missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacleManager == null || randomBoxManager == null)
            return;

        // 난이도 증가 → 스폰 간격 점점 짧게
        float distance = playerTransform.position.z;
        obstacleManager.spawnZDistance = Mathf.Clamp(maxSpawnDistance - distance * difficultyRampSpeed, minSpawnDistance, maxSpawnDistance);

        // RandomBox와의 충돌 방지
        float gap = Mathf.Abs(obstacleManager.LastSpawnZ - randomBoxManager.LastSpawnZ);
        if (gap < 10f)
        {
            randomBoxManager.DelayNextSpawn(1.0f);
        }
    }
}
