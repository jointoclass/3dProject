using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;          // 따라갈 플레이어의 Transform
    public Vector3 offset = new Vector3(0f, 10f, -10f);  // 플레이어와의 거리 (탑다운 뷰)
    public float smoothSpeed = 0.125f;  // 카메라 따라가는 속도 (부드러움 정도 조절)

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다!");
            return;
        }

        // 목표 위치 계산 (플레이어 위치 + 오프셋)
        Vector3 targetPosition = player.position + offset;

        // 현재 위치에서 목표 위치로 부드럽게 이동 (Lerp 사용)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // 카메라 위치 업데이트
        transform.position = smoothedPosition;

        // 플레이어를 항상 바라보도록 설정 (원하지 않으면 주석 처리 가능)
        transform.LookAt(player);
    }
}