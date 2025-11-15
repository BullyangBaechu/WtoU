using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlockWall : BaseObstacle
{
    protected override void OnPlayerEnter(PlayerController player)
    {
        // 벽과 처음 충돌 -> 이동 불가
        //player.SetCanMove(false);
        //player.forwardSpeed = 0f;
    }

    protected override void OnPlayerStay(PlayerController player)
    {
        // 부딪혀 있는 동안 계속 이동 금지
        //player.SetCanMove(false);
        //player.forwardSpeed = 0f;
    }

    protected override void OnPlayerExit(PlayerController player)
    {
        // 벽에서 떨어지는 순간 → 이동 가능
        //player.SetCanMove(true);

        // 원래 속도로 복구 (SlowZone 상태도 고려)
        if (player.IsInSlowZone())
            player.forwardSpeed = player.GetBaseSpeed() * player.GetSlowMultiplier();
        else
            player.forwardSpeed = player.GetBaseSpeed();
    }
}
