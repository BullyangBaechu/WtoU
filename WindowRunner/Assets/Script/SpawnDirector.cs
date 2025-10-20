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
    public float difficultyRampSpeed = 0.001f; // 플레이어 이동 거리에 따라 속도 증가
    public float minObstacleInterval = 0.8f;
    public float maxObstacleInterval = 2.0f;


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
        if (obstacleManager == null || randomBoxManager == null || playerTransform == null)
            return;

        // 두 스폰 간 거리 차이 검사 (겹치면 랜덤박스 스폰 약간 지연)
        float gap = Mathf.Abs(obstacleManager.LastSpawnZ - randomBoxManager.LastSpawnZ);
        if (gap < 10f)
        {
            randomBoxManager.DelayNextSpawn(1.0f);
        }

        // 플레이 거리 기반 난이도 조정 (장애물 스폰 주기 점점 빨라짐)
        float distance = playerTransform.position.z;
        obstacleManager.spawnInterval = Mathf.Clamp(
            maxObstacleInterval - (distance * difficultyRampSpeed),
            minObstacleInterval,
            maxObstacleInterval
        );
    }
}
