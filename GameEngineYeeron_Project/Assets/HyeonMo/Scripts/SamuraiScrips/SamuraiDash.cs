using System.Collections;
using UnityEngine;

public class SamuraiDash : PlayerDashInfermation {
    public GameObject afterImage; //�ܻ�, ������ �Ҵ�
    public GameObject enhanceAfterImage; //��ȭ�� ��ÿ� 4���� ������ ������Ʈ �Ҵ��� ����, ������ �Ҵ�
    public GameObject sword; //��, ������ �Ҵ�
    public GameObject flooring; //����, ������ �Ҵ�

    [HideInInspector] public bool canEnhanceAfterImage = false;
    public bool canRotateSword = false;
    public bool canInstallFlooring = false;

    //int index = 0; //�ݺ��� ����

    float enhanceAfterImageSpeed = 4.0f;
    public float swordRotationScale = 1.0f; //SamuraiReward���� ���
    public float swordRotationSpeed = 128f; //�⺻ 128f

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
        if (canInstallFlooring) { StartCoroutine(InstallFlooring()); }

        dashDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z).normalized;
        //transform.position += playerDashValue = new Vector3(PlayerMove.x * dashSpeed, 0, PlayerMove.z * dashSpeed); //�ڷ���Ʈ ���, ���� ����ȭ�� ���� �ʾ�, �밢���� �� �� ���� ����
        transform.position += playerDashValue = dashDirection * dashSpeed;
        isdashing = false;
    }

    void EnhanceAfterImage() { //�⺻ ����� �ܻ�ȿ�� ��ȭ
        //enhanceAfterImage.vel
    }

    void RotateSword() { //player �ֺ��� ���� ȸ���ϴ� �ɷ�, ȸ�� �ӵ� ������ SamuraiReward�� ����
        sword.transform.RotateAround(transform.position, SwordRotateVector, swordRotationSpeed * swordRotationScale * Time.fixedDeltaTime);
    }

    //void InstallFlooring() { //���� ��ġ
    //    StartCoroutine(SummenFlooring());
    //}


    IEnumerator SummenAfterImage() {
        playerPosition = transform.position; //��� �� ��ġ �ʱ�ȭ
        yield return new WaitForSeconds(0.03125f); //�ణ�� ��ȯ �ð� ���� �ο�

        Instantiate(afterImage, playerPosition, Quaternion.identity);
    }

    IEnumerator InstallFlooring() {
        playerPosition = transform.position; //��� �� ��ġ �ʱ�ȭ
        yield return new WaitForSeconds(0.03125f); //�ణ�� ��ȯ �ð� ���� �ο�

        Instantiate(flooring, playerPosition, Quaternion.identity);
    }
}

