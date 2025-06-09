using System.Collections;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    public GameObject afterImage; //잔상, 프리팹 할당
    public GameObject sword; //검, 프리팹 할당
    public GameObject flooring; //장판, 프리팹 할당

    [HideInInspector] public bool canEnhanceAfterImage = false;
    public bool canRotateSword = false;
    public bool canInstallFlooring = false;

    //int index = 0; //반복문 사용용
    public float swordRotationScale = 1.0f; //SamuraiReward에서 사용

    public float swordRotationSpeed = 128f; //기본 128f

    Vector3 playerPosition;
    Vector3 SwordRotateVector = Vector3.up;

    void Start() {
        if (sword == null) { sword = transform.GetChild(2).gameObject; }
        dashSpeed /= 2; //약간 대시 간격을 줄임
    }

    void FixedUpdate() {
        if (canRotateSword) { RotateSword(); }
    }

    public void Dash() {
        StartCoroutine(SummenAfterImage());
        if (canInstallFlooring) { StartCoroutine(InstallFlooring()); }

        transform.position += playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed); //텔레포트 대시
        isdashing = false;
    }

    void EnhanceAfterImage() { //기본 대시의 잔상효과 강화

    }

    void RotateSword() { //player 주변에 검이 회전하는 능력, 회전 속도 증가는 SamuraiReward에 존재
        sword.transform.RotateAround(transform.position, SwordRotateVector, swordRotationSpeed * swordRotationScale * Time.fixedDeltaTime);
    }

    //void InstallFlooring() { //장판 설치
    //    StartCoroutine(SummenFlooring());
    //}


    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //대시 전 위치 초기화
        yield return new WaitForSeconds(0.03125f); //약간의 소환 시간 간격 부여

        Instantiate(afterImage, playerPosition, Quaternion.identity);
    }

    IEnumerator InstallFlooring() {
        playerPosition = transform.position; //대시 전 위치 초기화
        yield return new WaitForSeconds(0.03125f); //약간의 소환 시간 간격 부여

        Instantiate(flooring, playerPosition, Quaternion.identity);
    }
}

