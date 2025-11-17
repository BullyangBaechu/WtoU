using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSliceManager : MonoBehaviour
{
    public Transform player;                    // 플레이어 Transform
    public List<GroundSlice> slices;            // 등록한 GroundSlice 리스트

    private float sliceLength;

    
    void Start()
    {
        if (slices.Count > 0)
            sliceLength = slices[0].sliceLength;
        else
            Debug.LogError("GroundSliceManager: slices 리스트가 비어있습니다!");
    }

    
    void Update()
    {
        foreach (var slice in slices)
        {
            // 플레이어가 GroundSlice를 지나쳤는지 판단
            if (player.position.z - slice.transform.position.z > sliceLength)
            {
                float newZ = GetFrontMostZ() + sliceLength;

                slice.transform.position = new Vector3(slice.transform.position.x, slice.transform.position.y, newZ);
            }
        }
    }

    // 가장 앞쪽 GroundSlice 위치 return
    private float GetFrontMostZ()
    {
        float maxZ = float.MinValue;
        foreach (var s in slices)
        {
            if (s.transform.position.z > maxZ)
                maxZ = s.transform.position.z;
        }
        return maxZ;
    }
}
