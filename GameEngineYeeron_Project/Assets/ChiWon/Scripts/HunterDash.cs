using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HunterDash : PlayerDashInfermation
{
    bool isInputDash;
    public static bool isdashing = false;
    Vector3 playerDashValue;
    public Rigidbody rigidbody; //Ȯ�ο�, ���� private ���� ����
    public Collider collider; //Ȯ�ο�, ���� private ���� ����


    public GameObject bulletPrefab;
    BulletInformation bulletInformation;
    [SerializeField] float bulletSpeed = 10.0f;
    [SerializeField] float bulletLifeTime = 2.0f;
    public bool getPenetration = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (dashCurrentStack < dashMaxStack)
        {     //������ ��ð� 2 �̻��̸� ��� ��Ÿ�� ī���� ����
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
            if (collider.enabled == true && PlayerInfermation.isHit)   //���� ���°� �ƴ� ��� ����
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;

        }
    }

    //void ShootingBullet()
    //{
    //    Vector3[] directions = new Vector3[]
    //    {
    //        new Vector3(0, 0, 1),
    //        new Vector3(1, 0, 1).normalized,
    //        new Vector3(1, 0, 0),
    //        new Vector3(1, 0, -1).normalized,
    //        new Vector3(0, 0, -1),
    //        new Vector3(-1, 0, -1).normalized,
    //        new Vector3(-1, 0, 0),
    //        new Vector3(-1, 0, 1).normalized
    //    };


    //    foreach (Vector3 direct in directions)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    //        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
    //        rigidbody.velocity = direct * bulletSpeed;

    //        Destroy(bullet, bulletLifeTime);
    //    }


    //    Debug.Log("Gun fired!");
    //}
    void ShootingBullet()
    {
        // 8����� ����: 360�� / 8 = 45��
        for (int i = 0; i < 8; i++)
        {
            // ���� ������ ��ȯ (0, 45, 90, �� 315 ��)
            float angleRad = i * 2 * Mathf.PI / 8f;

            // XZ ��鿡���� ���� ����
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), 0f, Mathf.Sin(angleRad)).normalized;

            SpawnBullet(dir);
        }
    }

    void SpawnBullet(Vector3 dir)
    {
        // �÷��̾� ��ġ���� ����
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // �߷� ���� Y�� ����
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;

            // ���ϴ� �ӵ��� �߻�
            rb.velocity = dir * bulletSpeed;
        }
        Destroy(bullet, bulletLifeTime);

        // (����) ������ ����� �������� �� �信�� Ȯ�ο����� �����
        Debug.DrawRay(transform.position, dir * 2f, Color.red, 1f);
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
        ShootingBullet();

    }

    IEnumerator ShotInvicible()
    {
        collider.enabled = false;   //����
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //���� ����
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
}
