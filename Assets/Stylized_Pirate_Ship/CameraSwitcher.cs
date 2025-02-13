using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public float transitionSpeed = 2.0f; // 카메라 이동 속도
    public GameObject character;

    private static bool isSwitching = false;
    private Camera activeCamera;
    private Animator animator;

    void Start()
    {
        // 초기 활성화 설정
        activeCamera = camera1;
        camera1.enabled = true;
        camera2.enabled = false;

        animator = character.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // 스페이스 키를 누르면 카메라 전환 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isSwitching)
        {
            StartCoroutine(SmoothSwitchCamera());
        }
        if (!isSwitching && activeCamera == camera2 && Input.GetMouseButton(0))
        {
            StartCoroutine(SmoothSwitchCamera());
        }
        if (activeCamera == camera2)
        {
            animator.Play("Gesture");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    IEnumerator SmoothSwitchCamera()
    {
        isSwitching = true;
        Camera oldCamera = activeCamera;
        Camera newCamera = (activeCamera == camera1) ? camera2 : camera1;

        Vector3 startPosition = oldCamera.transform.position;
        Quaternion startRotation = oldCamera.transform.rotation;
        Vector3 targetPosition = newCamera.transform.position;
        Quaternion targetRotation = newCamera.transform.rotation;

        // 새 카메라의 위치를 먼저 원래 카메라 위치로 맞춘다.
        newCamera.transform.position = startPosition;
        newCamera.transform.rotation = startRotation;
        newCamera.enabled = true;

        float transitionProgress = 0.0f;

        while (transitionProgress < 1.0f)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
            newCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, transitionProgress);
            newCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, transitionProgress);
            yield return null;
        }

        // 전환 완료 후 이전 카메라 비활성화
        oldCamera.enabled = false;
        activeCamera = newCamera;
        isSwitching = false;
    }
}
