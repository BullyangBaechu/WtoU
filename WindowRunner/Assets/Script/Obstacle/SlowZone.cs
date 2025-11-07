using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [Header("Slow Settings")]
    [Range(0.1f, 1f)]
    public float slowMultiplier = 0.5f; // 감속 비율
    public float slowDuration = 2f;     // 지속 시간

    private bool isActive = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.ApplySpeedModifier(slowMultiplier, slowDuration);

            // 즉시 풀로 복귀시킬 수도 있음 (일회성 장애물이라면)
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = false;
        gameObject.SetActive(false);
    }
}
