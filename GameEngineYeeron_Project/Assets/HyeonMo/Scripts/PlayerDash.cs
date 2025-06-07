using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    [SerializeField] GameObject attackObject; // 프리팹으로 반드시 할당할 것


    public void Dash() {
        attackObject.SetActive(true); //공격 수단 활성화
        playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed);
        rigidbody.AddForce(playerDashValue, ForceMode.Impulse);

        StartCoroutine(StopDash());
    }

    IEnumerator StopDash() {
        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        attackObject.SetActive(false); //공격 수단 비활성화
        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }
}