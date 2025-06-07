using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    [SerializeField] GameObject attackObject; // ���������� �ݵ�� �Ҵ��� ��


    public void Dash() {
        attackObject.SetActive(true); //���� ���� Ȱ��ȭ
        playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed);
        rigidbody.AddForce(playerDashValue, ForceMode.Impulse);

        StartCoroutine(StopDash());
    }

    IEnumerator StopDash() {
        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        attackObject.SetActive(false); //���� ���� ��Ȱ��ȭ
        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }
}