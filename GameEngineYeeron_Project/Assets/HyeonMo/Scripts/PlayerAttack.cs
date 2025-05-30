using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    EnemyStatus enemyStatus;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();
            Debug.Log("�̰� �� �Ҵ�Ȱ� ����?" + enemyStatus);

            enemyStatus.currentHP -= PlayerInfermation.attackPower;
            Debug.Log("Enemy �� �𿴱���");
        }
    }
}