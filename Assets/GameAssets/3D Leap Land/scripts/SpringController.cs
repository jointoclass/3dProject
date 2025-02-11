using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    public SkinnedMeshRenderer springMeshRenderer;  // 스프링 메쉬의 SkinnedMeshRenderer
    private int stretchedBlendShapeIndex;           // 'Stretched' BlendShape 인덱스
    public float stretchSpeed = 20f;                 // 늘어나는 속도
    public float maxStretch = 100f;                 // 최대 블렌드셰이프 값 (완전 늘어난 상태)
    public float recoverySpeed = 2f;                // 원래 상태로 돌아오는 속도

    private bool playerOnSpring = false;            // 플레이어가 스프링 위에 있는지 여부
    private float currentStretch = 0f;              // 현재 스프링 블렌드셰이프 값

    public float launchForce = 500f;                // 플레이어를 발사할 힘
    private bool isLaunching = false;               // 중복 실행 방지 플래그

    void Start()
    {
        springMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        stretchedBlendShapeIndex = springMeshRenderer.sharedMesh.GetBlendShapeIndex("Stretched");

        if (stretchedBlendShapeIndex == -1)
        {
            Debug.LogError("'Stretched'라는 BlendShape를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (playerOnSpring)
        {
            // 플레이어가 올라갔을 때 블렌드셰이프 값을 늘리기
            currentStretch = Mathf.Lerp(currentStretch, maxStretch, Time.deltaTime * stretchSpeed);
            if (currentStretch > 99)
            {
                playerOnSpring = false;
            }
        }
        else
        {
            // 플레이어가 내려가면 원래대로 돌아오기
            currentStretch = Mathf.Lerp(currentStretch, 0f, Time.deltaTime * recoverySpeed);
        }

        // 블렌드셰이프 값 적용
        springMeshRenderer.SetBlendShapeWeight(stretchedBlendShapeIndex, currentStretch);
    }

    // 플레이어가 스프링 위로 올라왔을 때 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("점프");
            playerOnSpring = true;
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * launchForce);
        }
    }    
}
