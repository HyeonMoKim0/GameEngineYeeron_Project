using UnityEngine;

public class EnhanceAfterImageDestroyer : MonoBehaviour {
    //EnhanceAfterImage �θ�� ��ũ��Ʈ
    void Start() {
        Destroy(gameObject, 5f); //5�� �� enhanceAfterImage�� �θ� ����� ��� enhanceAfterImage �Ҹ�
    }
}