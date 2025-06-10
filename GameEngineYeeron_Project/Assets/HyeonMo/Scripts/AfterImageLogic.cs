using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageLogic : MonoBehaviour {
    [Header("�ܻ� �ӵ�")]
    [SerializeField] float speedValue = 256f;

    GameObject player;

    Rigidbody rigidbody;

    EnemyStatus enemyStatus;

    Vector3 playerCurrentPosition;
    Vector3 myCurrenition;
    Vector3 diron;


    void Start(){
        player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(TracePlayer());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            enemyStatus.currentHP -= PlayerInfermation.attackPower;
        }
        else if (other.gameObject.CompareTag("Player")) {
            StopCoroutine(TracePlayer());
            Destroy(gameObject);
        }
        else {
            Debug.Log("�ٸ� ������Ʈ���� �浹 �νĿ� ������ ������ϴ�.");
            StopCoroutine(TracePlayer());
            Destroy(gameObject);
        }
    }

    IEnumerator TracePlayer() {
        yield return new WaitForSeconds(0.5f); //�� �ڸ��� �ܻ� ������ 0.5�� ���� ǥ��


        while (gameObject != null) {
            yield return new WaitForSeconds(0.03125f);

            CalculateDirection();
            speedValue += 2f; //���� ���� �ӵ��� �ø��� ����
            rigidbody.velocity = direction * speedValue;
        }
    }

    void CalculateDirection() {
        playerCurrentPosition = player.transform.position;
        myCurrentPosition = transform.position;
        direction = (playerCurrentPosition - myCurrentPosition).normalized;
    }

    //5�ʵ��� �����鼭, 1�ʸ��� ���ظ� �شٸ�
    //1�ʸ��� �Լ��� �����ϰ� ����
    //���⼭ �� ������ �ִ� �༮�鿡�Ը� ���ظ� �ְ� �Ѵٸ�, n�� �������� �����ִ°� ���� ������ ��?
    //�����ÿ����� �����ִ� ���� �����ϸ�, �ش� ������ ������Ʈ�� ��ȯ�ϰ�, �� ������Ʈ�� �浹�ϸ鼭�� �ش� �Լ��� bool�� �˻��Ͽ� on�� �� ���ظ� �������� ��
    //�̷� ����̸� ������ ������ ��
    //�׷��� �ܻ� ª�� �������� �����ֵ��� ����� ������ ������ ���δ�.
    //�̰� OnTriggerEnter�� �� ����, ������ �ְ� �ϴ� ������Ʈ�� Collider�� SetActive(false)�� �ߴٰ�, �ٽ� SetActive(true)�� �ϸ� �� �� �ϴ�.
    //tlqkf ������, �����ؼ� ������ �Ѵ�..
}