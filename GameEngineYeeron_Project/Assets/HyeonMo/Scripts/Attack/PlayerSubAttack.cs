using UnityEngine;

public class PlayerSubAttack : MonoBehaviour {
    EnemyStatus enemyStatus;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            enemyStatus.currentHP -= (PlayerInfermation.attackPower / 2); //보조로 공격하는 능력들은 공격력의 50%를 가지므로, 임시 방편으로 해당 스크립트 이용
            Debug.Log("서브 공격 적중");
        }
    }
}