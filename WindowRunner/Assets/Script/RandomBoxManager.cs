using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxManager : MonoBehaviour
{

    public static RandomBoxManager Instance;

    [Header("Random Box Settings")]
    public List<GameObject> boxPrefabs;
    public Transform playerTransform;
    public float spawnInterval = 6f;
    public float spawnZDistance = 35f;
    public float yPosition = 1f;

    private float timer = 0f;
    public float LastSpawnZ { get; private set; } = -999f;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomBox();
            timer = 0f;
        }
    }

    void SpawnRandomBox()
    {
        if (boxPrefabs.Count == 0 || playerTransform == null)
            return;

        int lane = Random.Range(-1, 2);
        float x = lane * GlobalSetting.laneOffset;
        float z = playerTransform.position.z + spawnZDistance;

        // 장애물과 너무 가까우면 스폰 스킵
        if (Mathf.Abs(z - ObstacleManager.Instance.LastSpawnZ) < 10f)
            return;

        int prefabIndex = Random.Range(0, boxPrefabs.Count);
        GameObject selectedBox = boxPrefabs[prefabIndex];

        Instantiate(selectedBox, new Vector3(x, yPosition, z), Quaternion.identity);

        LastSpawnZ = z;
    }

    // 필요 시 SpawnDirector에서 호출 가능
    public void DelayNextSpawn(float delay)
    {
        timer -= delay;
    }
}
