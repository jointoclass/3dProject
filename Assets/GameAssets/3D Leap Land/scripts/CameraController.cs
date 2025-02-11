using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;                         // 따라갈 플레이어의 Transform
    public Vector3 offset = new Vector3(0f, 4f, -4f); // 플레이어와의 거리
    public float smoothSpeed = 0.125f;               // 카메라 따라가는 속도
    public float movementThreshold = 0.05f;          // 미세한 움직임에 대한 임계값

    private Vector3 lastTargetPosition;              // 이전 플레이어 위치 저장
    private float initialXRotation = 0f;                  // 초기 X축 회전 값 저장

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다!");
            return;
        }

        // 초기 플레이어 위치 저장
        lastTargetPosition = player.position;

        // 초기 X축 회전 값 저장
        initialXRotation = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // 목표 위치 계산 (플레이어 위치 + 오프셋)
        Vector3 targetPosition = player.position + offset;
        float distanceMoved = Vector3.Distance(lastTargetPosition, targetPosition);

        if (distanceMoved > movementThreshold)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 마지막 위치 업데이트
            lastTargetPosition = player.position;
        }

        // 플레이어를 바라보되 X축 회전은 고정
        Vector3 currentRotation = transform.eulerAngles;
        transform.LookAt(player);
        if (initialXRotation == 0f)
        {
            initialXRotation = transform.eulerAngles.x;
        }

        // X축 회전을 초기값으로 고정
        transform.rotation = Quaternion.Euler(initialXRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
