using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [Header("Speed Settings")]
    public float forwardSpeed = 5f;         // 기본 전진 속도
    public float laneChangeSpeed = 10f;     // 레인 변경 속도

    [Header("Jump Settings")]
    public float jumpForce = 7f;            // 점프력
    //public float gravity = -9.81f;          // 필요하면 사용

    [Header("HP Settings")]
    public int maxHP = 3;
    public int currentHP = 3;

    [Header("PowerUps")]
    public float magnetDuration = 5f;
    public float boostMultiplier = 1.5f;
}
