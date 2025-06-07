using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSkill : MonoBehaviour
{
    [Header("�ڵ� �߻� ����")]
    public GameObject autoBulletPrefab; // �ڵ� �߻�� źȯ ������ (BulletInformation ����)
    public float currentShootInterval = 5f; // ���� �ڵ� �߻� ��Ÿ�� (�ʱ� 5��)
    public float minShootInterval = 1f; // �ڵ� �߻� �ּ� ��Ÿ�� (1��)
    public float shootIntervalDecreaseAmount = 1f; // ��Ÿ�� ���ҷ� (1��)
    public int numberOfAutoBullets = 5; // �� ���� �߻��� źȯ �� (�ʱ� 5��)
    public float autoBulletSpreadAngle = 45f; // ��ä�� ������ �� ����

    private Coroutine autoShootCoroutine; // �ڵ� �߻� �ڷ�ƾ ����

    void Start()
    {
        // �ڵ� �߻� �ڷ�ƾ ����
        autoShootCoroutine = StartCoroutine(AutoShootRoutine());
    }

    /// <summary>
    /// ������ ���ݸ��� �ڵ����� źȯ�� �߻��ϴ� �ڷ�ƾ�Դϴ�.
    /// </summary>
    IEnumerator AutoShootRoutine()
    {
        while (true) // ������ ����Ǵ� ���� ��� �ݺ�
        {
            yield return new WaitForSeconds(currentShootInterval); // ���� ��Ÿ�Ӹ�ŭ ���
            ShootAutoBullets(); // źȯ �߻� �Լ� ȣ��
        }
    }

    /// <summary>
    /// ��ä�� ���·� źȯ�� �߻��ϴ� �Լ��Դϴ�.
    /// </summary>
    void ShootAutoBullets()
    {
        float startAngle = -autoBulletSpreadAngle / 2f;
        float angleStep = (numberOfAutoBullets > 1) ? (autoBulletSpreadAngle / (numberOfAutoBullets - 1)) : 0f;

        // �÷��̾ �ٶ󺸴� ���� (=�̵��ϴ� ���� ��)�� �������� źȯ�� �߻�
        Vector3 playerForwardDirection = new Vector3(PlayerMove.x, 0, PlayerMove.z);
        if (playerForwardDirection == Vector3.zero) // �÷��̾ �������� �ʾ� transform.forward�� 0�� ��� ���
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
                // BulletInformation�� �ִ� �ӵ� ���� ����մϴ�.
                Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bulletInstance.transform.forward * bulletInfo.bulletSpeed;
                }
                // źȯ ����, ���ݷ�, ���� ���δ� BulletInformation ��ũ��Ʈ���� ��ü������ �����մϴ�.
            }
            else
            {
                Debug.LogError("�ڵ� �߻� źȯ �����տ� 'BulletInformation' ������Ʈ�� �����ϴ�. źȯ�� ����� �۵����� ���� �� �ֽ��ϴ�.", bulletInstance);
                // BulletInformation�� ���� ��츦 ����� �⺻ �ı� (������ BulletInformation���� �ı��ϴ� ���� ����)
                Destroy(bulletInstance, 2.0f);
            }
        }
    }

    /// <summary>
    /// �ڵ� �߻� ��Ÿ���� 1�� ���ҽ�Ű�� �Լ� (�������� ȣ��� �Լ�)
    /// </summary>
    public void DecreaseAutoShootCooldownReward()
    {
        if (currentShootInterval > minShootInterval) // �ּ� ��Ÿ�� (1��)���� Ŭ ���� ����
        {
            currentShootInterval -= shootIntervalDecreaseAmount;
            // ��Ÿ���� 1�� �̸����� �������� �ʵ��� ����
            if (currentShootInterval < minShootInterval)
            {
                currentShootInterval = minShootInterval;
            }

            // ���� �ڷ�ƾ�� ���߰� ���ο� ��Ÿ������ �ٽ� ����
            if (autoShootCoroutine != null)
            {
                StopCoroutine(autoShootCoroutine);
            }
            autoShootCoroutine = StartCoroutine(AutoShootRoutine());

            Debug.Log($"�ڵ� �߻� ��Ÿ�� ����! ���� ��Ÿ��: {currentShootInterval}��");
        }
        else
        {
            Debug.Log("�ڵ� �߻� ��Ÿ���� �̹� �ּ��Դϴ� (1��). �� �̻� ������ �� �����ϴ�.");
        }
    }

    /// <summary>
    /// �ڵ� �߻� ��Ÿ�� ���� ���� �������� ���� �� �ִ��� Ȯ���ϴ� �Լ� (UI ��� ȣ��)
    /// </summary>
    public bool CanDecreaseAutoShootCooldown()
    {
        // ��Ÿ���� 1�ʺ��� Ŭ ���� ���� �������� ����
        return currentShootInterval > minShootInterval;
    }

    /// <summary>
    /// �ڵ� �߻� źȯ ���� ������Ű�� �Լ� (��û�� ���� 'ź 5�� ����' ��� �߰�)
    /// </summary>
    public void IncreaseAutoBulletCountReward(int amount = 5)
    {
        numberOfAutoBullets += amount;
        Debug.Log($"�ڵ� �߻� źȯ �� ����! ���� źȯ ��: {numberOfAutoBullets}��");
    }

    // ���� �� ������� ���� �ð�ȭ (�� �信�� �߻� ���� Ȯ�ο�)
    void OnDrawGizmos()
    {
        if (Application.isPlaying) // ���� ���� ���� ����
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
