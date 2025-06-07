using System.Collections;
using System.Linq;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    public GameObject afterImage; //�ܻ�, ������ �Ҵ�

    Vector3 playerPosition;

    public void Dash() {
        StartCoroutine(SummenAfterImage());

        transform.position += playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed); //�ڷ���Ʈ ���
        isdashing = false;
    }

    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //��� �� ��ġ �ʱ�ȭ
        yield return new WaitForSeconds(0.03125f); //�ణ�� ��ȯ �ð� ���� �ο�

        Instantiate(afterImage, playerPosition, Quaternion.identity);
    }
}
