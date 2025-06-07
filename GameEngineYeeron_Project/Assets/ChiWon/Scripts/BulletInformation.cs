using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInformation : MonoBehaviour
{
    private Rigidbody bulletRigidbody; // private으로 변경하여 외부에서 직접 접근 방지
    private EnemyStatus enemyStatus;
    private Collider bulletCollider;
    private Transform playerTransform;

    [Header("탄환 능력치")]
    public int bulletAdk = 1; // ★탄환의 최종 공격력! (예: 50%라면 50을 직접 입력)
    public float bulletSpeed = 60f; // ★탄환 속도 (인스펙터에서 조절)
    public bool penetration = false; // ★탄환 관통 여부 (기본값 false, 보상으로 true 변경 가능)

    [Header("콜라이더 활성화 설정")]
    public float enableColliderDistance = 1.5f; // 플레이어로부터 얼마나 떨어지면 콜라이더 활성화할지

    [Header("탄환 수명")]
    public float bulletLifetime = 2.0f; // 탄환이 파괴되기까지의 시간 (인스펙터에서 조절)

    private void Awake() // Start()보다 먼저 호출되어 안정적으로 컴포넌트 참조
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();

        // "Player" 태그를 가진 오브젝트를 찾아 플레이어 Transform을 가져옵니다.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("씬에 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다. 거리 기반 콜라이더 활성화가 작동하지 않을 수 있습니다.");
        }

        // 초기에는 콜라이더를 비활성화합니다.
        if (bulletCollider != null)
        {
            bulletCollider.enabled = false;
        }

        // 설정된 수명 후 탄환을 자동으로 파괴합니다.
        Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
        // 콜라이더가 비활성화되어 있고, 플레이어가 유효하며, 충분히 멀리 떨어졌을 경우 콜라이더 활성화
        if (bulletCollider != null && !bulletCollider.enabled && playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance >= enableColliderDistance)
            {
                bulletCollider.enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 이미 파괴된 오브젝트는 처리하지 않습니다.
        if (gameObject == null) return;

        if (other.CompareTag("Enemy"))
        {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // BulletInformation 외에 공격 계산은 하지 않으므로, 이 곳에서 바로 데미지 적용
                enemyStatus.currentHP -= bulletAdk; // 적에게 데미지 적용
                Debug.Log($"적과 충돌! 남은 HP: {enemyStatus.currentHP}");

                if (!penetration)
                {
                    Destroy(gameObject); // 관통 속성이 없으면 탄환 파괴
                }
                // 관통 속성이 true면 탄환은 파괴되지 않고 계속 나아갑니다.
            }
            else
            {
                Debug.LogWarning($"적 오브젝트 '{other.gameObject.name}'에 'EnemyStatus' 컴포넌트가 없습니다.", other.gameObject);
            }
        }
        // "Wall" 또는 "Obstacle" 태그에 닿으면 탄환 제거 (맵 외곽 벽 처리)
        else if (other.CompareTag("Wall")) // Obstacle 태그도 고려하여 추가
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 탄환의 관통 속성을 설정하는 함수 (보상 등으로 외부에서 호출 가능)
    /// </summary>
    public void SetPenetration(bool value)
    {
        penetration = value;
    }
}