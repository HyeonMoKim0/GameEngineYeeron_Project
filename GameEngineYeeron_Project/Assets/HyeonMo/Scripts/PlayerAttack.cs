using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    EnemyStatus enemyStatus;
    PlayerInfermation playerInfermation;
    void Start() {
        playerInfermation = GetComponent<PlayerInfermation>();
    }

    void OnTriggerEnter(Collider other) {
        if (PlayerDash.isdashing && other.gameObject.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            enemyStatus.currentHP -= PlayerInfermation.attackPower;
        }
    }
}