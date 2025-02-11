using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer; // Skinned Mesh Renderer를 할당합니다.
    private int flyBlendShapeIndex; // Fly 블렌드셰이프 인덱스 저장
    public float animationSpeed = 200f; // 블렌드셰이프 애니메이션 속도 조절

    private float currentWeight = 0f;
    private bool increasing = true;

    public float moveDistance = 5f;   // 벌이 이동할 거리
    public float moveSpeed = 2f;      // 이동 속도
    private Vector3 startPosition;    // 시작 위치 저장
    private Vector3 targetPosition;   // 목표 위치 저장
    private bool movingForward = true; // 이동 방향

    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        flyBlendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Fly");

        if (flyBlendShapeIndex == -1)
        {
            Debug.LogError("'Fly'라는 BlendShape를 찾을 수 없습니다.");
        }

        startPosition = transform.position;
        targetPosition = startPosition + transform.forward * moveDistance;
    }

    void Update()
    {
        AnimateBlendShape(); // 애니메이션 컨트롤
        MoveBee(); // 이동 컨트롤
    }

    void AnimateBlendShape()
    {
        if (increasing)
        {
            currentWeight += animationSpeed * Time.deltaTime;
            if (currentWeight >= 100f)
            {
                currentWeight = 100f;
                increasing = false;
            }
        }
        else
        {
            currentWeight -= animationSpeed * Time.deltaTime;
            if (currentWeight <= 0f)
            {
                currentWeight = 0f;
                increasing = true;
            }
        }

        skinnedMeshRenderer.SetBlendShapeWeight(flyBlendShapeIndex, currentWeight);
    }

    void MoveBee()
    {
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingForward = false;
                // 벌이 돌아설 때 방향 전환 (180도 회전)
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                movingForward = true;
                // 다시 돌아설 때 방향 전환 (원래 방향으로 회전)
                transform.Rotate(0f, 180f, 0f);
            }
        }
    }
}
