using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    // 카메라가 따라갈 타겟
    public Transform target;

    // 카메라 상대적 위치
    public Vector3 offset = new Vector3(0, 6, -8);

    // 카메라 속도
    public float followSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desirePostion = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desirePostion, Time.deltaTime * followSpeed);

        // 카메라가 타겟 바라보기
        //transform.LookAt(target);
        transform.LookAt(target.position + target.forward * 10f);
    }
}
