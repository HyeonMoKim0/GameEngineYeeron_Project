using UnityEngine;

public class Flooring : MonoBehaviour {
    [SerializeField] Transform playerTransform; //player�� transform �������� LookAt ���
    [SerializeField] Collider collider; //�ڽ��� collider, �ǹ��� �ڽ����� �����ϴ� �ļ��� ����߱� ����

    public float destroyTime = 1.1f; //�ణ�� ���� ���� ������ �߻��� �� �־�, 0.1�� �߰�
    float attackDelay = 0;
    

    void Start() {
        if (playerTransform != GameObject.Find("Player").GetComponent<Transform>()) { playerTransform = GameObject.Find("Player").GetComponent<Transform>(); }
        transform.LookAt(playerTransform); //�÷��̾��� �������� �ٶ󺸹Ƿ�, ���ǵ� ��ġ�� �ٲ� ��

        if (collider != GetComponentInChildren<Collider>()) { collider = GetComponentInChildren<Collider>(); }
        Destroy(gameObject, destroyTime);
    }


    void Update() {
        attackDelay += Time.deltaTime;
        if (attackDelay > 1.0f) {
            collider.enabled = false;
            collider.enabled = true;
            attackDelay = 0f;
        }
    }

    
}