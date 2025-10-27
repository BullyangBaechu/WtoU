using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class BlockWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 이동 즉시 정지
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.SetCanMove(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 부딪혀있는 동안 계속 이동 금지 유지
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.SetCanMove(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 벽에서 벗어나면 이동 다시 가능
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.SetCanMove(true);

            // 풀로 반환
            SimpleObjectPool.Instance.ReturnToPool(this.gameObject);
        }
    }


}
