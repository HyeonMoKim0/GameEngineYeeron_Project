using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInformation : MonoBehaviour
{
    private Rigidbody bulletRigidbody; // private���� �����Ͽ� �ܺο��� ���� ���� ����
    private EnemyStatus enemyStatus;
    private Collider bulletCollider;
    private Transform playerTransform;

    [Header("źȯ �ɷ�ġ")]
    public int bulletAdk = 1; // ��źȯ�� ���� ���ݷ�! (��: 50%��� 50�� ���� �Է�)
    public float bulletSpeed = 60f; // ��źȯ �ӵ� (�ν����Ϳ��� ����)
    public bool penetration = false; // ��źȯ ���� ���� (�⺻�� false, �������� true ���� ����)

    [Header("�ݶ��̴� Ȱ��ȭ ����")]
    public float enableColliderDistance = 1.5f; // �÷��̾�κ��� �󸶳� �������� �ݶ��̴� Ȱ��ȭ����

    [Header("źȯ ����")]
    public float bulletLifetime = 2.0f; // źȯ�� �ı��Ǳ������ �ð� (�ν����Ϳ��� ����)

    private void Awake() // Start()���� ���� ȣ��Ǿ� ���������� ������Ʈ ����
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<Collider>();

        // "Player" �±׸� ���� ������Ʈ�� ã�� �÷��̾� Transform�� �����ɴϴ�.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("���� 'Player' �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�. �Ÿ� ��� �ݶ��̴� Ȱ��ȭ�� �۵����� ���� �� �ֽ��ϴ�.");
        }

        // �ʱ⿡�� �ݶ��̴��� ��Ȱ��ȭ�մϴ�.
        if (bulletCollider != null)
        {
            bulletCollider.enabled = false;
        }

        // ������ ���� �� źȯ�� �ڵ����� �ı��մϴ�.
        Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
        // �ݶ��̴��� ��Ȱ��ȭ�Ǿ� �ְ�, �÷��̾ ��ȿ�ϸ�, ����� �ָ� �������� ��� �ݶ��̴� Ȱ��ȭ
        if (bulletCollider != null && !bulletCollider.enabled && playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance >= enableColliderDistance)
            {
                bulletCollider.enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �̹� �ı��� ������Ʈ�� ó������ �ʽ��ϴ�.
        if (gameObject == null) return;

        if (other.CompareTag("Enemy"))
        {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            if (enemyStatus != null)
            {
                // BulletInformation �ܿ� ���� ����� ���� �����Ƿ�, �� ������ �ٷ� ������ ����
                enemyStatus.currentHP -= bulletAdk; // ������ ������ ����
                Debug.Log($"���� �浹! ���� HP: {enemyStatus.currentHP}");

                if (!penetration)
                {
                    Destroy(gameObject); // ���� �Ӽ��� ������ źȯ �ı�
                }
                // ���� �Ӽ��� true�� źȯ�� �ı����� �ʰ� ��� ���ư��ϴ�.
            }
            else
            {
                Debug.LogWarning($"�� ������Ʈ '{other.gameObject.name}'�� 'EnemyStatus' ������Ʈ�� �����ϴ�.", other.gameObject);
            }
        }
        // "Wall" �Ǵ� "Obstacle" �±׿� ������ źȯ ���� (�� �ܰ� �� ó��)
        else if (other.CompareTag("Wall")) // Obstacle �±׵� ����Ͽ� �߰�
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// źȯ�� ���� �Ӽ��� �����ϴ� �Լ� (���� ������ �ܺο��� ȣ�� ����)
    /// </summary>
    public void SetPenetration(bool value)
    {
        penetration = value;
    }
}