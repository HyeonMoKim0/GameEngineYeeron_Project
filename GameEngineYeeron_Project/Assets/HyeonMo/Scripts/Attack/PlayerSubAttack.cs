using UnityEngine;

public class PlayerSubAttack : MonoBehaviour {
    EnemyStatus enemyStatus;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            enemyStatus = other.gameObject.GetComponent<EnemyStatus>();

            enemyStatus.currentHP -= (PlayerInfermation.attackPower / 2); //������ �����ϴ� �ɷµ��� ���ݷ��� 50%�� �����Ƿ�, �ӽ� �������� �ش� ��ũ��Ʈ �̿�
            Debug.Log("���� ���� ����");
        }
    }
}