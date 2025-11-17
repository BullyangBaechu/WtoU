using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : BaseObstacle
{
    [Header("Slider Settings")]
    public float moveSpeed = 2f;          // 이동 속도
    public int laneA = -1;                // 시작에서 막는 레인 1
    public int laneB = 0;                 // 시작에서 막는 레인 2

    public int targetLaneA = 0;           // 이동해서 막을 레인 1
    public int targetLaneB = 1;           // 이동해서 막을 레인 2

    private Vector3 startPos;
    private Vector3 endPos;

    private float t = 0f;
    private int direction = 1;

    protected override void OnEnable()
    {
        base.OnEnable();

        startPos = LaneCenter(laneA, laneB);
        endPos = LaneCenter(targetLaneA, targetLaneB);

        // 시작 위치로 초기화 (z, y는 기존 값 유지)
        transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);

        t = 0f;
        direction = 1;
    }

    void Update()
    {
        if (!isActive)
            return;

        t += Time.deltaTime * moveSpeed * direction;

        // 왕복 처리
        if (t > 1f)
        {
            t = 1f;
            direction = -1;
        }
        else if (t < 0f)
        {
            t = 0f;
            direction = 1;
        }

        float x = Mathf.Lerp(startPos.x, endPos.x, t);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    // 두 레인을 커버하는 장애물의 X 중심값 계산
    Vector3 LaneCenter(int l1, int l2)
    {
        float x1 = l1 * GlobalSetting.laneOffset;
        float x2 = l2 * GlobalSetting.laneOffset;
        float centerX = (x1 + x2) * 0.5f;

        return new Vector3(centerX, transform.position.y, transform.position.z);
    }

    protected override void OnPlayerEnter(PlayerController player)
    {
        //player.SetCanMove(false);
    }

    protected override void OnPlayerExit(PlayerController player)
    {
        //player.SetCanMove(true);
    }

    protected override void OnPlayerStay(PlayerController player)
    {
    }
}
