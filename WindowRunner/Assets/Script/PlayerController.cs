using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float forwardSpeed = 5f;
    public float laneChangeSpeed = 10f;

    [Header("Lane Settings")]
    public float laneOffset = 10f;
    private int currentLane = 0;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.1f;

    [Header("Ground Detection")]
    public LayerMask groundLayer; // Ground 전용 감지용 레이어 마스크

    private Rigidbody rb;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
        HandleLaneMove();
    }

    void FixedUpdate()
    {
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // 전진 이동
    void MoveForward()
    {
        rb.MovePosition(rb.position + Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
    }

    // 라인 보간 이동
    void HandleLaneMove()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, Time.deltaTime * laneChangeSpeed);
        transform.position = newPosition;
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("장애물 충돌!");
            forwardSpeed = 0f;
        }
    }
}
