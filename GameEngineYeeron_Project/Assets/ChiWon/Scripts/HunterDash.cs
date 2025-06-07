using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HunterDash : PlayerDashInfermation
{
    bool isInputDash;
    public static bool isdashing = false;
    Vector3 playerDashValue;
    public Rigidbody rigidbody; //확인용, 추후 private 변경 예정
    public Collider collider; //확인용, 추후 private 변경 예정


    public GameObject bulletPrefab;
    BulletInformation bulletInformation;
    [SerializeField] float bulletSpeed = 10.0f;
    [SerializeField] float bulletLifeTime = 2.0f;
    public bool getPenetration = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (dashCurrentStack < dashMaxStack)
        {     //충전된 대시가 2 이상이면 대시 쿨타임 카운팅 시작
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
            if (collider.enabled == true && PlayerInfermation.isHit)   //무적 상태가 아닐 경우 실행
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;

        }
    }

    //void ShootingBullet()
    //{
    //    Vector3[] directions = new Vector3[]
    //    {
    //        new Vector3(0, 0, 1),
    //        new Vector3(1, 0, 1).normalized,
    //        new Vector3(1, 0, 0),
    //        new Vector3(1, 0, -1).normalized,
    //        new Vector3(0, 0, -1),
    //        new Vector3(-1, 0, -1).normalized,
    //        new Vector3(-1, 0, 0),
    //        new Vector3(-1, 0, 1).normalized
    //    };


    //    foreach (Vector3 direct in directions)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    //        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
    //        rigidbody.velocity = direct * bulletSpeed;

    //        Destroy(bullet, bulletLifeTime);
    //    }


    //    Debug.Log("Gun fired!");
    //}
    void ShootingBullet()
    {
        // 8등분한 각도: 360° / 8 = 45°
        for (int i = 0; i < 8; i++)
        {
            // 라디안 단위로 변환 (0, 45, 90, … 315 도)
            float angleRad = i * 2 * Mathf.PI / 8f;

            // XZ 평면에서의 방향 벡터
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad)).normalized;

            SpawnBullet(dir);
        }
    }

    void SpawnBullet(Vector3 dir)
    {
        // 플레이어 위치에서 생성
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 중력 끄고 Y축 고정
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            // 원하는 속도로 발사
            rb.velocity = dir * bulletSpeed;
        }
        Destroy(bullet, bulletLifeTime);

        // (선택) 방향이 제대로 나가는지 씬 뷰에서 확인용으로 디버그
        Debug.DrawRay(transform.position, dir * 2f, Color.red, 1f);
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
        ShootingBullet();

    }

    IEnumerator ShotInvicible()
    {
        collider.enabled = false;   //무적
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //무적 해제
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
}
