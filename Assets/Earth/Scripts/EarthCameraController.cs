using UnityEngine;

public class EarthCameraController : MonoBehaviour
{
    public Transform target; // 지구 3D 모델의 Transform
    public float rotationSpeed = 1f; // 회전 속도 (더 낮게 설정)
    public float zoomSpeed = 2.0f; // 줌 속도
    public float minZoomDistance = 30.0f; // 최소 줌 거리
    public float maxZoomDistance = 40.0f; // 최대 줌 거리

    private float currentZoom = 40.0f; // 초기 줌 거리
    private Vector3 currentRotation; // 현재 회전 각도
    private Vector3 targetRotation;  // 목표 회전 각도
    public float smoothTime = 0.1f; // 회전 부드러움 설정 시간
    private Vector3 velocity = Vector3.zero; // 회전 감속을 위한 속도 변수

    void Start()
    {
        currentRotation = transform.eulerAngles; // 초기 회전 각도 설정
        targetRotation = currentRotation; // 목표 회전 각도 초기화
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(0)) // 왼쪽 마우스 버튼 클릭 시 회전
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            targetRotation.x += mouseY;
            targetRotation.y += mouseX;

            // X축(상하 회전)의 각도를 제한 (카메라가 뒤집히지 않도록)
            targetRotation.x = Mathf.Clamp(targetRotation.x, -90, 90);
        }

        // 부드러운 회전 적용 (SmoothDamp 사용)
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref velocity, smoothTime);

        // 카메라 위치 및 방향 설정
        Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        transform.position = target.position - rotation * Vector3.forward * currentZoom;
        transform.LookAt(target.position); // 타겟(지구)을 항상 바라보도록 설정
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom -= scroll;
        currentZoom = Mathf.Clamp(currentZoom, minZoomDistance, maxZoomDistance); // 줌 범위 제한
    }
}
