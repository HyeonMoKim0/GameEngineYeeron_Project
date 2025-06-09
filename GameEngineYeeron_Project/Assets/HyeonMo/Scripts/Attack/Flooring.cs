using UnityEngine;

public class Flooring : MonoBehaviour {
    [SerializeField] Transform playerTransform; //player�� transform �������� LookAt ���
    [SerializeField] Collider collider; //�ڽ��� collider

    public float destroyTime = 1.1f; //�ణ�� ���� ���� ������ �߻��� �� �־�, 0.1�� �߰�
    float attackDelay = 0;
    

    void Start() {
        if (playerTransform == null) { playerTransform = gameObject.transform.Find("Player").GetComponent<Transform>(); }
        transform.LookAt(playerTransform);
        if (collider == null) { collider = GetComponent<Collider>(); }
        Destroy(transform.parent.gameObject, destroyTime);
        
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