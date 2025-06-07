using System.Collections;
using System.Linq;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    public GameObject afterImage; //잔상, 프리팹 할당

    Vector3 playerPosition;

    public void Dash() {
        StartCoroutine(SummenAfterImage());

        transform.position += playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed); //텔레포트 대시
        isdashing = false;
    }

    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //대시 전 위치 초기화
        yield return new WaitForSeconds(0.03125f); //약간의 소환 시간 간격 부여

        Instantiate(afterImage, playerPosition, Quaternion.identity);
    }
}
