using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public float transitionSpeed = 2.0f; // ī�޶� �̵� �ӵ�
    public GameObject character;
    public GameObject baloon;

    private static bool isSwitching = false;
    private Camera activeCamera;
    private Animator animator;

    void Start()
    {
        // �ʱ� Ȱ��ȭ ����
        activeCamera = camera1;
        camera1.enabled = true;
        camera2.enabled = false;

        animator = character.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // �����̽� Ű�� ������ ī�޶� ��ȯ ����
        if (Input.GetMouseButtonDown(0) && activeCamera == camera1 && !isSwitching)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == baloon)
            {
                StartCoroutine(SmoothSwitchCamera());
            }
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

        // �� ī�޶��� ��ġ�� ���� ���� ī�޶� ��ġ�� �����.
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

        // ��ȯ �Ϸ� �� ���� ī�޶� ��Ȱ��ȭ
        oldCamera.enabled = false;
        activeCamera = newCamera;
        isSwitching = false;
    }
}
