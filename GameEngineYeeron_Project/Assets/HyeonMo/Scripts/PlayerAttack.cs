using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    EnemyStatus enemyStatus;
    PlayerInfermation playerInfermation;
    void Start() {
        playerInfermation = GetComponent<PlayerInfermation>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();
            Debug.Log("�̰� �� �Ҵ�Ȱ� ����?" + enemyStatus);

            enemyStatus.currentHP -= playerInfermation.attackPower;
            Debug.Log("Enemy �� �𿴱���");
        }
    }
}