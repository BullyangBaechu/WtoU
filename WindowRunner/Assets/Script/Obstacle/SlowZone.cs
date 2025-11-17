using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : BaseObstacle
{
    public float slowMultiplier = 0.5f;
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    protected override void OnPlayerEnter(PlayerController player)
    {
        // 장판에 진입한 순간부터 감속 시작
        player.EnterSlowZone(slowMultiplier);
    }

    protected override void OnPlayerStay(PlayerController player)
    {
        // 장판 위에 있는 동안 감속 유지
        player.MaintainSlowZone();
    }

    protected override void OnPlayerExit(PlayerController player)
    {
        // 장판을 벗어난 순간 → 원래 속도로 복구
        player.ExitSlowZone();
    }
}
