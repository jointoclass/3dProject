using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;  // 발사할 포탄 프리팹
    public Transform firePoint;          // 포탄이 발사될 위치 (대포의 입구)
    public float fireForce = 500f;       // 포탄 발사 힘
    public float fireInterval = 2f;      // 발사 간격 (1초)

    private void Start()
    {
        // 1초마다 포탄 발사
        InvokeRepeating(nameof(FireProjectile), 0f, fireInterval);        
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile Prefab 또는 Fire Point가 할당되지 않았습니다.");
            return;
        }

        // 180도 회전 추가 (Y축 기준)
        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 180f, 0);

        // 180도 회전된 포탄 생성
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
        Destroy(projectile, 2f);

        // 포탄에 Rigidbody가 있는지 확인 후 force 적용
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce);
        }
        else
        {
            Debug.LogError("Projectile Prefab에 Rigidbody가 없습니다.");
        }
    }
}
