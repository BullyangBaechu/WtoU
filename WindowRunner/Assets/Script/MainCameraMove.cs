using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    // ī�޶� ���� Ÿ��
    public Transform target;

    // ī�޶� ����� ��ġ
    public Vector3 offset = new Vector3(0, 6, -8);

    // ī�޶� �ӵ�
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

        // ī�޶� Ÿ�� �ٶ󺸱�
        //transform.LookAt(target);
        transform.LookAt(target.position + target.forward * 10f);
    }
}
