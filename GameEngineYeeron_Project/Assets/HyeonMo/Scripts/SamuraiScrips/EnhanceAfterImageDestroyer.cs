using UnityEngine;

public class EnhanceAfterImageDestroyer : MonoBehaviour {
    //EnhanceAfterImage 부모용 스크립트
    void Start() {
        Destroy(gameObject, 5f); //5초 뒤 enhanceAfterImage의 부모가 사라져 모든 enhanceAfterImage 소멸
    }
}