using System.Collections;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    [SerializeField] GameObject afterImage; //잔상, 프리팹 할당
    [SerializeField] GameObject enhanceAfterImage; //강화된 대시에 4가지 방향의 오브젝트 할당을 위함, 프리팹 할당
    [SerializeField] GameObject sword; //검, 프리팹 할당
    [SerializeField] GameObject flooring; //장판, 프리팹 할당


    public bool canEnhanceAfterImage = false;
    public bool canRotateSword = false;
    public bool canInstallFlooring = false;

    [SerializeField] float summenDelay = 0.03125f;
    [SerializeField] float swordRotationSpeed = 128f; //기본 128f
    public float swordRotationScale = 1.0f; //SamuraiReward에서 사용


    Vector3 dashDirection;
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

        dashDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z).normalized;
        transform.position += playerDashValue = dashDirection * dashSpeed;
        isdashing = false;
    }


    void RotateSword() { //player 주변에 검이 회전하는 능력, 회전 속도 증가는 SamuraiReward에 존재
        sword.transform.RotateAround(transform.position, SwordRotateVector, swordRotationSpeed * swordRotationScale * Time.fixedDeltaTime);
    }


    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //대시 전 위치 초기화
        yield return new WaitForSeconds(summenDelay); //약간의 소환 시간 간격 부여

        Instantiate(afterImage, playerPosition, Quaternion.identity);
        if (canEnhanceAfterImage) { Instantiate(enhanceAfterImage, playerPosition, Quaternion.identity); }
        if (canInstallFlooring) { Instantiate(flooring, playerPosition, Quaternion.identity); }
    }

    //IEnumerator InstallFlooring() {
    //    playerPosition = transform.position; //대시 전 위치 초기화
    //    yield return new WaitForSeconds(summenDelay); //약간의 소환 시간 간격 부여

    //    Instantiate(flooring, playerPosition, Quaternion.identity);
    //}
}

