using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSkill : MonoBehaviour
{
    [Header("자동 발사 설정")]
    public GameObject autoBulletPrefab; // 자동 발사용 탄환 프리팹 (BulletInformation 포함)
    public float currentShootInterval = 5f; // 현재 자동 발사 쿨타임 (초기 5초)
    public float minShootInterval = 1f; // 자동 발사 최소 쿨타임 (1초)
    public float shootIntervalDecreaseAmount = 1f; // 쿨타임 감소량 (1초)
    public int numberOfAutoBullets = 5; // 한 번에 발사할 탄환 수 (초기 5발)
    public float autoBulletSpreadAngle = 45f; // 부채꼴 형태의 총 각도

    private Coroutine autoShootCoroutine; // 자동 발사 코루틴 참조

    void Start()
    {
        // 자동 발사 코루틴 시작
        autoShootCoroutine = StartCoroutine(AutoShootRoutine());
    }

    /// <summary>
    /// 지정된 간격마다 자동으로 탄환을 발사하는 코루틴입니다.
    /// </summary>
    IEnumerator AutoShootRoutine()
    {
        while (true) // 게임이 실행되는 동안 계속 반복
        {
            yield return new WaitForSeconds(currentShootInterval); // 현재 쿨타임만큼 대기
            ShootAutoBullets(); // 탄환 발사 함수 호출
        }
    }

    /// <summary>
    /// 부채꼴 형태로 탄환을 발사하는 함수입니다.
    /// </summary>
    void ShootAutoBullets()
    {
        float startAngle = -autoBulletSpreadAngle / 2f;
        float angleStep = (numberOfAutoBullets > 1) ? (autoBulletSpreadAngle / (numberOfAutoBullets - 1)) : 0f;

        // 플레이어가 바라보는 방향 (=이동하는 방향 앞)을 기준으로 탄환을 발사
        Vector3 playerForwardDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z);
        if (playerForwardDirection == Vector3.zero) // 플레이어가 움직이지 않아 transform.forward가 0일 경우 대비
        {
            playerForwardDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z);
        }

        for (int i = 0; i < numberOfAutoBullets; i++)
        {
            float currentBulletAngle = startAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.LookRotation(playerForwardDirection) * Quaternion.Euler(0, currentBulletAngle, 0);

            GameObject bulletInstance = Instantiate(autoBulletPrefab, transform.position, bulletRotation);

            BulletInformation bulletInfo = bulletInstance.GetComponent<BulletInformation>();
            if (bulletInfo != null)
            {
                // BulletInformation에 있는 속도 값을 사용합니다.
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bulletInstance.transform.forward * bulletInfo.bulletSpeed;
                }
                // 탄환 수명, 공격력, 관통 여부는 BulletInformation 스크립트에서 자체적으로 관리합니다.
            }
            else
            {
                Debug.LogError("자동 발사 탄환 프리팹에 'BulletInformation' 컴포넌트가 없습니다. 탄환이 제대로 작동하지 않을 수 있습니다.", bulletInstance);
                // BulletInformation이 없을 경우를 대비한 기본 파괴 (하지만 BulletInformation에서 파괴하는 것이 좋음)
                Destroy(bulletInstance, 2.0f);
            }
        }
    }

    /// <summary>
    /// 자동 발사 쿨타임을 1초 감소시키는 함수 (보상으로 호출될 함수)
    /// </summary>
    public void DecreaseAutoShootCooldownReward()
    {
        if (currentShootInterval > minShootInterval) // 최소 쿨타임 (1초)보다 클 때만 감소
        {
            currentShootInterval -= shootIntervalDecreaseAmount;
            // 쿨타임이 1초 미만으로 내려가지 않도록 보정
            if (currentShootInterval < minShootInterval)
            {
                currentShootInterval = minShootInterval;
            }

            // 기존 코루틴을 멈추고 새로운 쿨타임으로 다시 시작
            if (autoShootCoroutine != null)
            {
                StopCoroutine(autoShootCoroutine);
            }
            autoShootCoroutine = StartCoroutine(AutoShootRoutine());

            Debug.Log($"자동 발사 쿨타임 감소! 현재 쿨타임: {currentShootInterval}초");
        }
        else
        {
            Debug.Log("자동 발사 쿨타임이 이미 최소입니다 (1초). 더 이상 감소할 수 없습니다.");
        }
    }

    /// <summary>
    /// 자동 발사 쿨타임 감소 보상 선택지가 나올 수 있는지 확인하는 함수 (UI 등에서 호출)
    /// </summary>
    public bool CanDecreaseAutoShootCooldown()
    {
        // 쿨타임이 1초보다 클 때만 보상 선택지가 나옴
        return currentShootInterval > minShootInterval;
    }

    /// <summary>
    /// 자동 발사 탄환 수를 증가시키는 함수 (요청에 따라 '탄 5발 증가' 기능 추가)
    /// </summary>
    public void IncreaseAutoBulletCountReward(int amount = 5)
    {
        numberOfAutoBullets += amount;
        Debug.Log($"자동 발사 탄환 수 증가! 현재 탄환 수: {numberOfAutoBullets}발");
    }

    // 개발 중 디버깅을 위한 시각화 (씬 뷰에서 발사 방향 확인용)
    void OnDrawGizmos()
    {
        if (Application.isPlaying) // 게임 실행 중일 때만
        {
            Gizmos.color = Color.yellow;
            Vector3 forwardDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z);
            if (forwardDirection == Vector3.zero) { forwardDirection = Vector3.forward; }

            float startAngle = -autoBulletSpreadAngle / 2f;
            float endAngle = autoBulletSpreadAngle / 2f;

            Quaternion startRotation = Quaternion.LookRotation(forwardDirection) * Quaternion.Euler(0, startAngle, 0);
            Quaternion endRotation = Quaternion.LookRotation(forwardDirection) * Quaternion.Euler(0, endAngle, 0);

            Gizmos.DrawRay(transform.position, startRotation * Vector3.forward * 5f);
            Gizmos.DrawRay(transform.position, endRotation * Vector3.forward * 5f);
        }
    }
}
