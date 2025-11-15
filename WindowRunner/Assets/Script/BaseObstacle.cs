using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class BaseObstacle : MonoBehaviour
{

    protected bool isActive = true;
    protected virtual void Awake()
    {
        // 장애물 공통 물리 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    protected virtual void OnEnable()
    {
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerEnter(collision.gameObject.GetComponent<PlayerController>());
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isActive) return;
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerStay(collision.gameObject.GetComponent<PlayerController>());
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isActive) return;
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerExit(collision.gameObject.GetComponent<PlayerController>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other.CompareTag("Player"))
            OnPlayerEnter(other.GetComponent<PlayerController>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;
        if (other.CompareTag("Player"))
            OnPlayerStay(other.GetComponent<PlayerController>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (other.CompareTag("Player"))
            OnPlayerExit(other.GetComponent<PlayerController>());
    }

    // 자식 클래스가 반드시 구현해야 하는 부분
    protected abstract void OnPlayerEnter(PlayerController player);
    protected abstract void OnPlayerStay(PlayerController player);
    protected abstract void OnPlayerExit(PlayerController player);

    // ObstacleManager에게 불려서 회수됨
    public virtual void Deactivate()
    {
        isActive = false;
        SimpleObjectPool.Instance.ReturnToPool(this.gameObject);
    }
}
