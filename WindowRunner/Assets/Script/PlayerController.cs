using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    [Header("Lane Settings")]
    public float laneOffset = 10f;
    private int currentLane = 0;

    [Header("Jump Settings")]
    //public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f;

    [Header("Ground Detection")]
    public LayerMask groundLayer; // Ground 전용 감지용 레이어 마스크

    // 캐릭터 스탯
    public PlayerStats stats;

    private Rigidbody rb;
    private Vector3 targetPosition;

    // 이동 방해 관련
    private bool canMove = true;

    // 실제 이동 속도 관리
    private float currentForwardSpeed;
   
    // 이속 감속 관련
    private float baseForwardSpeed;
    private bool isInSlowZone = false;
    private float slowMultiplier = 0.5f;

    public bool IsInSlowZone() => isInSlowZone;
    public float GetBaseSpeed() => baseForwardSpeed;
    public float GetSlowMultiplier() => slowMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;

        baseForwardSpeed = stats.forwardSpeed;
        currentForwardSpeed = stats.forwardSpeed;
    }

    void Update()
    {
        if (!canMove)
            return;

        HandleInput();
        HandleLaneMove();   
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        MoveForward();
    }

    // 입력 처리
    void HandleInput()
    {
        // 좌우 이동 입력
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > -1)
        {
            currentLane--;
            UpdateTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 1)
        {
            currentLane++;
            UpdateTargetPosition();
        }

        // 점프 입력 (Ground 레이어만 감지)
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
        }
    }

    // 전진 
    void MoveForward()
    {
        rb.MovePosition(rb.position + Vector3.forward * currentForwardSpeed * Time.fixedDeltaTime);
    }

    // 라인 이동
    void HandleLaneMove()
    {
        Vector3 target = new Vector3(currentLane * laneOffset, transform.position.y, transform.position.z);
        Vector3 newPos = Vector3.Lerp(transform.position, target, Time.deltaTime * stats.laneChangeSpeed);

        rb.MovePosition(new Vector3(newPos.x, rb.position.y, rb.position.z));
    }

    // 타겟 위치 갱신
    void UpdateTargetPosition()
    {
        targetPosition = new Vector3(currentLane * laneOffset, transform.position.y, transform.position.z);
    }

    // Ground 레이어 전용 Raycast 감지
    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.green);
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // 장애물 충돌 처리
    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("장애물 충돌!");
            forwardSpeed = 0f;
        }
        */
    }

    // BlockWall 관련 로직 함수
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    // SlowZone 관련 로직 함수
    public void EnterSlowZone(float multiplier)
    {
        isInSlowZone = true;
        slowMultiplier = multiplier;
        currentForwardSpeed = baseForwardSpeed * slowMultiplier;
    }

    public void MaintainSlowZone()
    {
        if (isInSlowZone)
            currentForwardSpeed = baseForwardSpeed * slowMultiplier;
    }

    public void ExitSlowZone()
    {
        isInSlowZone = false;
        currentForwardSpeed = baseForwardSpeed;
    }

    public void SetCurrentSpeed(float value)
    {
        currentForwardSpeed = value;
    }

}
