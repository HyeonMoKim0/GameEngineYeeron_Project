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
            Debug.Log("이거 잘 할당된거 맞죠?" + enemyStatus);

            enemyStatus.currentHP -= playerInfermation.attackPower;
            Debug.Log("Enemy 잘 깎였구요");
        }
    }
}