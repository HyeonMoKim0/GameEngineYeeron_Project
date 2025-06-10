using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HunterDash : PlayerDashInfermation
{
    // ���� ������ (���� ����)
    bool isInputDash;
    public static bool isdashing = false;
    Vector3 playerDashValue;
    public Rigidbody rigidbody; //Ȯ�ο�, ���� private ���� ����
    public Collider collider; //Ȯ�ο�, ���� private ���� ����

    public GameObject bulletPrefab;
    // BulletInformation bulletInformation; // �� ������ ������ �����Ƿ� �����ϰų� �ּ� ó�� ����
    [SerializeField] float bulletSpeed = 5.0f;
    [SerializeField] float bulletLifeTime = 2.0f;
    public bool getPenetration = false;

    // --- ������� ���� ��� ���� ���� �߰� ---
    [Header("Hunter Dash Bullet ���� (���� �߰�)")] // ����� �߰��Ͽ� ����
    [Tooltip("��� �� �߻�Ǵ� źȯ�� ���� �� �����Դϴ�.")]
    public int currentNumberOfDashBullets = 8; // �� �ʱ� 8���� źȯ �߻�

    [Header("��� źȯ ���׷��̵�")]
    [Tooltip("��� źȯ ���׷��̵� �� �߰��Ǵ� źȯ�� �� (��: 1�߾� �߰�)")]
    public int dashBulletUpgradeAmount = 1; // �� ���׷��̵�� ź�� 1�߾� �߰�

    [Tooltip("��� źȯ ���׷��̵� ���� �ִ� ȹ�� Ƚ��")]
    public int maxDashBulletUpgrades = 5; // ��� źȯ ���׷��̵� ���� �ִ� Ƚ�� (����)
    private int currentDashBulletUpgrades = 0; // ���� ��� źȯ ���׷��̵� ���� Ƚ��
    // --- ���� ��� ���� ���� �߰� �� ---


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (dashCurrentStack < dashMaxStack)
        {   //������ ��ð� 2 �̻��̸� ��� ��Ÿ�� ī���� ����
            redashCurrentTime += Time.deltaTime;
            dashCurrentDelay += Time.deltaTime;

            if (redashCurrentTime >= redashConstraintTime && dashCurrentStack < dashMaxStack)
            {
                ++dashCurrentStack;
                redashCurrentTime = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        isInputDash = Input.GetButton("Fire3"); //GetButton�� ���� �ν��� �ߵǾ� ����

        if (isInputDash && PlayerMove.IsMoving())
        {
            Dash(ref PlayerMove.x, ref PlayerMove.z);

        }

    }

    void Dash(ref float x, ref float z)
    {
        if (IsNotDelay() && IsExistDashStack())
        {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);

            StartCoroutine(StopDash());
            if (collider.enabled == true && PlayerInfermation.isHit)    //���� ���°� �ƴ� ��� ����
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;

        }
    }

    void ShootingBullet()
    {
        // źȯ�� ���� �۶߸��� ���� ���� ��� (360���� ���� źȯ ���� ����)
        float angleStep = 360f / currentNumberOfDashBullets; // �� currentNumberOfDashBullets ���

        // �÷��̾� ��ġ���� ����
        Vector3 playerPosition = transform.position;

        for (int i = 0; i < currentNumberOfDashBullets; i++) // �� currentNumberOfDashBullets ��ŭ źȯ �߻�
        {
            float currentBulletAngle = angleStep * i;

            // XZ ��鿡���� ���� ����
            Vector3 dir = new Vector3(Mathf.Cos(currentBulletAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(currentBulletAngle * Mathf.Deg2Rad)).normalized;

            SpawnBullet(dir);
        }
    }

    void SpawnBullet(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        BulletInformation bulletInfo = bullet.GetComponent<BulletInformation>(); // BulletInformation ����

        if (rb != null)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            if (bulletInfo != null)
            {
                // BulletInformation�� �ӵ� ��� �� ���� ���� ����
                rb.velocity = dir * bulletInfo.bulletSpeed;
                bulletInfo.SetPenetration(getPenetration); // HunterDash�� getPenetration ���� ����
            }
            else
            {
                // BulletInformation�� ������ HunterDash�� ��ü �ӵ� ���
                rb.velocity = dir * bulletSpeed;
                Debug.LogWarning("źȯ �����տ� 'BulletInformation' ������Ʈ�� �����ϴ�. HunterDash�� ��ü bulletSpeed�� ����մϴ�.");
            }
        }
        // Destroy(bullet, bulletLifeTime); // BulletInformation�� ������ �����ϹǷ� ���⼭ Destroy ȣ���� �ߺ��� �� �ֽ��ϴ�.
        // ���� BulletInformation���� Destroy�� ���� �ʴ´ٸ� �� ���� Ȱ��ȭ�ϼ���.
        Debug.DrawRay(transform.position, dir * 2f, Color.red, 1f);
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
        ShootingBullet(); // ��� ���� �� źȯ �߻�
    }

    IEnumerator ShotInvicible()
    {
        collider.enabled = false;    //����
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;     //���� ����
        PlayerInfermation.isHit = true;
    }

    bool IsNotDelay()
    {
        if (dashCurrentDelay >= dashAfterDelay) { return true; }
        else { return false; }
    }

    bool IsExistDashStack()
    {
        if (dashCurrentStack > dashMinStack) { return true; }
        else { return false; }
    }

    // --- ������� ���� ��� ���� �޼��� �߰� ---
    /// <summary>
    /// ��� źȯ�� ���׷��̵��ϴ� �Լ� (�������� ȣ��� �Լ�)
    /// ���׷��̵�� ź�� �� ���⸶�� 1�߾� �߰� (��: 8 -> 9 -> 10 ...)
    /// </summary>
    public void UpgradeDashBulletsReward()
    {
        // �ִ� ���׷��̵� Ƚ���� �ʰ����� �ʾҴ��� Ȯ��
        if (currentDashBulletUpgrades < maxDashBulletUpgrades)
        {
            currentNumberOfDashBullets += dashBulletUpgradeAmount; // �� źȯ �� ����
            currentDashBulletUpgrades++; // ȹ�� Ƚ�� ����
            Debug.Log($"��� źȯ ���׷��̵�! ���� ��� źȯ ��: {currentNumberOfDashBullets}��. ȹ�� Ƚ��: {currentDashBulletUpgrades}/{maxDashBulletUpgrades}");
        }
        else
        {
            Debug.Log($"��� źȯ ���׷��̵� ���� �ִ� Ƚ���� �����߽��ϴ� ({maxDashBulletUpgrades}ȸ).");
        }
    }

    /// <summary>
    /// ��� źȯ ���׷��̵� ���� �������� ���� �� �ִ��� Ȯ���ϴ� �Լ� (UI ��� ȣ��)
    /// </summary>
    public bool CanUpgradeDashBullets()
    {
        // �ִ� ���׷��̵� Ƚ���� �ʰ����� �ʾ��� ���� true ��ȯ
        return currentDashBulletUpgrades < maxDashBulletUpgrades;
    }
    // --- ���� ��� ���� �޼��� �߰� �� ---
}