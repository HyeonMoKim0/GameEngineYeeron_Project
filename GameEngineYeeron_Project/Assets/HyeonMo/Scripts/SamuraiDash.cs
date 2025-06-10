using System.Collections;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    [SerializeField] GameObject afterImage; //�ܻ�, ������ �Ҵ�
    [SerializeField] GameObject enhanceAfterImage; //��ȭ�� ��ÿ� 4���� ������ ������Ʈ �Ҵ��� ����, ������ �Ҵ�
    [SerializeField] GameObject sword; //��, ������ �Ҵ�
    [SerializeField] GameObject flooring; //����, ������ �Ҵ�


    public bool canEnhanceAfterImage = false;
    public bool canRotateSword = false;
    public bool canInstallFlooring = false;

    [SerializeField] float summenDelay = 0.03125f;
    [SerializeField] float swordRotationSpeed = 128f; //�⺻ 128f
    public float swordRotationScale = 1.0f; //SamuraiReward���� ���


    Vector3 dashDirection;
    Vector3 playerPosition;
    Vector3 SwordRotateVector = Vector3.up;
    

    void Start() {
        if (sword == null) { sword = transform.GetChild(2).gameObject; }
        dashSpeed /= 2; //�ణ ��� ������ ����
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


    void RotateSword() { //player �ֺ��� ���� ȸ���ϴ� �ɷ�, ȸ�� �ӵ� ������ SamuraiReward�� ����
        sword.transform.RotateAround(transform.position, SwordRotateVector, swordRotationSpeed * swordRotationScale * Time.fixedDeltaTime);
    }


    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //��� �� ��ġ �ʱ�ȭ
        yield return new WaitForSeconds(summenDelay); //�ణ�� ��ȯ �ð� ���� �ο�

        Instantiate(afterImage, playerPosition, Quaternion.identity);
        if (canEnhanceAfterImage) { Instantiate(enhanceAfterImage, playerPosition, Quaternion.identity); }
        if (canInstallFlooring) { Instantiate(flooring, playerPosition, Quaternion.identity); }
    }

    //IEnumerator InstallFlooring() {
    //    playerPosition = transform.position; //��� �� ��ġ �ʱ�ȭ
    //    yield return new WaitForSeconds(summenDelay); //�ణ�� ��ȯ �ð� ���� �ο�

    //    Instantiate(flooring, playerPosition, Quaternion.identity);
    //}
}

