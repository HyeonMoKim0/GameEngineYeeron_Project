using UnityEngine;

public class Flooring : MonoBehaviour {
    [SerializeField] Transform playerTransform; //player의 transform 가져오고 LookAt 사용
    [SerializeField] Collider collider; //자신의 collider

    public float destroyTime = 1.1f; //약간의 공격 간격 오차가 발생할 수 있어, 0.1초 추가
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