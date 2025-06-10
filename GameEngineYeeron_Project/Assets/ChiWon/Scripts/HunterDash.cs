using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HunterDash : PlayerDashInfermation
{
    // 기존 변수들 (수정 없음)
    bool isInputDash;
    public static bool isdashing = false;
    Vector3 playerDashValue;
    public Rigidbody rigidbody; //확인용, 추후 private 변경 예정
    public Collider collider; //확인용, 추후 private 변경 예정

    public GameObject bulletPrefab;
    // BulletInformation bulletInformation; // 이 변수는 사용되지 않으므로 제거하거나 주석 처리 권장
    [SerializeField] float bulletSpeed = 5.0f;
    [SerializeField] float bulletLifeTime = 2.0f;
    public bool getPenetration = false;

    // --- 여기부터 보상 기능 관련 변수 추가 ---
    [Header("Hunter Dash Bullet 설정 (보상 추가)")] // 헤더를 추가하여 구분
    [Tooltip("대시 시 발사되는 탄환의 현재 총 개수입니다.")]
    public int currentNumberOfDashBullets = 8; // ★ 초기 8방향 탄환 발사

    [Header("대시 탄환 업그레이드")]
    [Tooltip("대시 탄환 업그레이드 시 추가되는 탄환의 수 (예: 1발씩 추가)")]
    public int dashBulletUpgradeAmount = 1; // ★ 업그레이드시 탄이 1발씩 추가

    [Tooltip("대시 탄환 업그레이드 보상 최대 획득 횟수")]
    public int maxDashBulletUpgrades = 5; // 대시 탄환 업그레이드 보상 최대 횟수 (예시)
    private int currentDashBulletUpgrades = 0; // 현재 대시 탄환 업그레이드 보상 횟수
    // --- 보상 기능 관련 변수 추가 끝 ---


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (dashCurrentStack < dashMaxStack)
        {   //충전된 대시가 2 이상이면 대시 쿨타임 카운팅 시작
            redashCurrentTime += Time.deltaTime;
            dashCurrentDelay += Time.deltaTime;

            if (redashCurrentTime >= redashConstraintTime && dashCurrentStack < dashMaxStack)
            {
                ++dashCurrentStack;
                redashCurrentTime = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        isInputDash = Input.GetButton("Fire3"); //GetButton이 제일 인식이 잘되어 선택

        if (isInputDash && PlayerMove.IsMoving())
        {
            Dash(ref PlayerMove.x, ref PlayerMove.z);

        }

    }

    void Dash(ref float x, ref float z)
    {
        if (IsNotDelay() && IsExistDashStack())
        {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);

            StartCoroutine(StopDash());
            if (collider.enabled == true && PlayerInfermation.isHit)    //무적 상태가 아닐 경우 실행
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;

        }
    }

    void ShootingBullet()
    {
        // 탄환을 고르게 퍼뜨리기 위한 각도 계산 (360도를 현재 탄환 수로 나눔)
        float angleStep = 360f / currentNumberOfDashBullets; // ★ currentNumberOfDashBullets 사용

        // 플레이어 위치에서 생성
        Vector3 playerPosition = transform.position;

        for (int i = 0; i < currentNumberOfDashBullets; i++) // ★ currentNumberOfDashBullets 만큼 탄환 발사
        {
            float currentBulletAngle = angleStep * i;

            // XZ 평면에서의 방향 벡터
            Vector3 dir = new Vector3(Mathf.Cos(currentBulletAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(currentBulletAngle * Mathf.Deg2Rad)).normalized;

            SpawnBullet(dir);
        }
    }

    void SpawnBullet(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        BulletInformation bulletInfo = bullet.GetComponent<BulletInformation>(); // BulletInformation 참조

        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            if (bulletInfo != null)
            {
                // BulletInformation의 속도 사용 및 관통 여부 설정
                rb.velocity = dir * bulletInfo.bulletSpeed;
                bulletInfo.SetPenetration(getPenetration); // HunterDash의 getPenetration 값을 전달
            }
            else
            {
                // BulletInformation이 없으면 HunterDash의 자체 속도 사용
                rb.velocity = dir * bulletSpeed;
                Debug.LogWarning("탄환 프리팹에 'BulletInformation' 컴포넌트가 없습니다. HunterDash의 자체 bulletSpeed를 사용합니다.");
            }
        }
        // Destroy(bullet, bulletLifeTime); // BulletInformation이 수명을 관리하므로 여기서 Destroy 호출은 중복될 수 있습니다.
        // 만약 BulletInformation에서 Destroy를 하지 않는다면 이 줄을 활성화하세요.
        Debug.DrawRay(transform.position, dir * 2f, Color.red, 1f);
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
        ShootingBullet(); // 대시 종료 후 탄환 발사
    }

    IEnumerator ShotInvicible()
    {
        collider.enabled = false;    //무적
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;     //무적 해제
        PlayerInfermation.isHit = true;
    }

    bool IsNotDelay()
    {
        if (dashCurrentDelay >= dashAfterDelay) { return true; }
        else { return false; }
    }

    bool IsExistDashStack()
    {
        if (dashCurrentStack > dashMinStack) { return true; }
        else { return false; }
    }

    // --- 여기부터 보상 기능 관련 메서드 추가 ---
    /// <summary>
    /// 대시 탄환을 업그레이드하는 함수 (보상으로 호출될 함수)
    /// 업그레이드시 탄이 각 방향마다 1발씩 추가 (예: 8 -> 9 -> 10 ...)
    /// </summary>
    public void UpgradeDashBulletsReward()
    {
        // 최대 업그레이드 횟수를 초과하지 않았는지 확인
        if (currentDashBulletUpgrades < maxDashBulletUpgrades)
        {
            currentNumberOfDashBullets += dashBulletUpgradeAmount; // ★ 탄환 수 증가
            currentDashBulletUpgrades++; // 획득 횟수 증가
            Debug.Log($"대시 탄환 업그레이드! 현재 대시 탄환 수: {currentNumberOfDashBullets}발. 획득 횟수: {currentDashBulletUpgrades}/{maxDashBulletUpgrades}");
        }
        else
        {
            Debug.Log($"대시 탄환 업그레이드 보상 최대 횟수에 도달했습니다 ({maxDashBulletUpgrades}회).");
        }
    }

    /// <summary>
    /// 대시 탄환 업그레이드 보상 선택지가 나올 수 있는지 확인하는 함수 (UI 등에서 호출)
    /// </summary>
    public bool CanUpgradeDashBullets()
    {
        // 최대 업그레이드 횟수를 초과하지 않았을 때만 true 반환
        return currentDashBulletUpgrades < maxDashBulletUpgrades;
    }
    // --- 보상 기능 관련 메서드 추가 끝 ---
}