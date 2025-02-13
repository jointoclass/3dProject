using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f;  // 최대 이동 거리 (위아래 진폭)
    public float speed = 2.0f;      // 위아래 이동 속도

    private float initialY;
    private Transform mainCamera;

    void Start()
    {
        initialY = transform.position.y;  // 물체의 초기 Y 위치 저장
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        float newY = initialY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.LookAt(mainCamera);
    }
}
