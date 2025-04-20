using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float moveSpeed = 5f;
    public float laneChangeSpeed = 10f;
  
    public float laneOffset = 10f; // 좌우 라인 거리
    public float jumpForce = 7f;

    private Rigidbody rb;
    private int currentLane = 0; // -1 = 왼쪽, 0 = 중앙, 1 = 오른쪽
    private bool isJumping = false;

    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 자동 전진
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 좌우 이동(키입력)
        if(Input.GetKeyDown(KeyCode.LeftArrow)&& currentLane > -1)
        {
            currentLane -= 1;
            // MoveToLane();
            UpdateTargetPosition();
        }

        else if(Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 1)
        {
            currentLane += 1;
            // MoveToLane();
            UpdateTargetPosition();
        }

        // 점프
        if(Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, Time.deltaTime * laneChangeSpeed);
        transform.position = newPosition;
    }

    // 기본 순간 이동 코드
    void MoveToLane()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = currentLane * laneOffset;
        transform.position = newPosition;
    }

    // Lerp를 이용한 보간 이동
    void UpdateTargetPosition()
    {
        targetPosition = new Vector3(currentLane * laneOffset, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ground에 닿으면 점프 가능
        if (collision.gameObject.CompareTag("Ground"))
            isJumping = false;
    }
}
