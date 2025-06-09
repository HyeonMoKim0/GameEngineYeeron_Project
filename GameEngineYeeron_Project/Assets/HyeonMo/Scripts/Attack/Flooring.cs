using UnityEngine;

public class Flooring : MonoBehaviour {
    [SerializeField] Transform playerTransform; //player의 transform 가져오고 LookAt 사용
    [SerializeField] Collider collider; //자식의 collider, 피벗을 자식으로 조정하는 꼼수를 사용했기 때문

    public float destroyTime = 1.1f; //약간의 공격 간격 오차가 발생할 수 있어, 0.1초 추가
    float attackDelay = 0;
    

    void Start() {
        if (playerTransform != GameObject.Find("Player").GetComponent<Transform>()) { playerTransform = GameObject.Find("Player").GetComponent<Transform>(); }
        transform.LookAt(playerTransform); //플레이어의 방향으로 바라보므로, 장판도 위치가 바뀔 것

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