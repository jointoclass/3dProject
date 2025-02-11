using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리를 위해 추가

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;       // 걷기 속도
    public float jumpForce = 4f;       // 점프 힘
    private Rigidbody rb;              // Rigidbody 컴포넌트 참조
    private Animator animator;         // Animator 컴포넌트 참조
    private bool isGrounded = true;    // 플레이어가 땅에 있는지 여부
    private bool isDead = false;       // 플레이어 사망 상태 체크
    private bool isTrophy = false;       // 트로피 획득 체크

    private string currentAnimation = "Idle";  // 현재 재생 중인 애니메이션 이름

    private Vector3 movementInput;     // 입력값 저장 (FixedUpdate에서 사용)

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();       // Rigidbody 컴포넌트 가져오기
        animator = GetComponentInChildren<Animator>();  // Animator 컴포넌트 가져오기

        if (rb == null)
        {
            Debug.LogError("Rigidbody 컴포넌트가 없습니다!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 없습니다!");
        }
    }

    void Update()
    {
        if (isDead || isTrophy) return;  // 사망 상태에서는 입력 및 애니메이션 업데이트 중단

        // 이동 입력 처리
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movementInput = new Vector3(moveX, 0, moveZ).normalized;

        HandleJump(); // 점프 컨트롤
        UpdateAnimations(); // 애니메이션 컨트롤
    }

    void FixedUpdate()
    {
        if (isDead || isTrophy) return;  // 사망 상태에서는 물리 이동 중단

        MovePlayer();
    }

    // 플레이어 물리 이동 처리
    void MovePlayer()
    {
        Vector3 movement = movementInput * moveSpeed;
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        // 이동 방향으로 플레이어 회전
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    // 점프 처리
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // 점프 중 상태로 설정
            PlayAnimation("Jump");  // 점프 애니메이션 재생
        }
    }

    // 애니메이션 상태 업데이트
    void UpdateAnimations()
    {
        if (!isGrounded) return;  // 점프 중일 때는 다른 애니메이션 재생 안 함

        if (movementInput.magnitude > 0)
        {
            PlayAnimation("Walk");
        }
        else
        {
            PlayAnimation("Idle");
        }
    }

    // 애니메이션 재생 함수
    void PlayAnimation(string animationName)
    {
        if (currentAnimation == animationName) return;  // 중복 재생 방지

        animator.Play(animationName);
        currentAnimation = animationName;
    }

    // 충돌 처리 (Ground 및 Dead 태그)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground detected");
            if (!isGrounded)
            {
                isGrounded = true;  // 착지 시 점프 상태 해제
                PlayAnimation("Idle");
            }
        }

        if (collision.gameObject.CompareTag("Dead") && !isDead)
        {
            Debug.Log("Dead detected");
            HandleDeath();  // 사망 처리 함수 호출
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Trophy"))
        {
            HandleTrophy();
            Destroy(collision.gameObject);
        }
    }

    // 사망 처리 함수
    private void HandleDeath()
    {
        isDead = true;
        PlayAnimation("Dead");
        rb.velocity = Vector3.zero;  // 움직임 정지
        StartCoroutine(RestartSceneAfterDelay(3f));  // 3초 후 씬 재시작
    }

    private void HandleTrophy()
    {
        isTrophy = true;
        PlayAnimation("Trophy");
        rb.velocity = Vector3.zero;  // 움직임 정지
        StartCoroutine(RestartSceneAfterDelay(1.5f));  // 3초 후 씬 재시작
    }

    // 3초 후 씬 재시작 코루틴
    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
