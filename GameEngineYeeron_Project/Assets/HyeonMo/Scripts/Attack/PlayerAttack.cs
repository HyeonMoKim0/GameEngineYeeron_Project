using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    EnemyStatus enemyStatus;

    void OnTriggerEnter(Collider other) {
        //if (PlayerDashInfermation.isdashing && other.gameObject.CompareTag("Enemy")) { ���� �ڵ�
        if (other.gameObject.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            enemyStatus.currentHP -= PlayerInfermation.attackPower;
        }
    }
}