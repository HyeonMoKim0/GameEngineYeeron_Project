using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageLogic : MonoBehaviour {
    [Header("잔상 속도")]
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
            Debug.Log("다른 오브젝트와의 충돌 인식에 문제가 생겼습니다.");
            StopCoroutine(TracePlayer());
            Destroy(gameObject);
        }
    }

    IEnumerator TracePlayer() {
        yield return new WaitForSeconds(0.5f); //제 자리에 잔상 생김을 0.5초 동안 표시


        while (gameObject != null) {
            yield return new WaitForSeconds(0.03125f);

            CalculateDirection();
            speedValue += 2f; //점점 오는 속도를 늘리기 위함
            rigidbody.velocity = direction * speedValue;
        }
    }

    void CalculateDirection() {
        playerCurrentPosition = player.transform.position;
        myCurrentPosition = transform.position;
        direction = (playerCurrentPosition - myCurrentPosition).normalized;
    }

    //5초동안 있으면서, 1초마다 피해를 준다면
    //1초마다 함수를 실행하게 만듦
    //여기서 그 영역에 있는 녀석들에게만 피해를 주게 한다면, n초 간격으로 피해주는거 구현 가능할 듯?
    //메이플에서도 피해주는 것을 생각하면, 해당 범위의 오브젝트를 소환하고, 그 오브젝트와 충돌하면서도 해당 함수가 bool로 검사하여 on일 때 피해를 입히도록 함
    //이런 방식이면 구현은 가능할 듯
    //그러면 잔상도 짧은 간격으로 피해주도록 만들면 가능할 것으로 보인다.
    //이걸 OnTriggerEnter로 한 다음, 공격을 주게 하는 오브젝트의 Collider를 SetActive(false)로 했다가, 다시 SetActive(true)로 하면 될 듯 하다.
    //tlqkf 문제는, 가능해서 만들어야 한다..
}