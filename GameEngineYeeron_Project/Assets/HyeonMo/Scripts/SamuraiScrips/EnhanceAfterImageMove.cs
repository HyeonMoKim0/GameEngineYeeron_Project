using UnityEngine;

enum rotationYAngle {
    is45 = 1,
    is135,
    is225,
    is315
}

public class EnhanceAfterImageMove : MonoBehaviour {
    [SerializeField] float speed = 2f;

    Rigidbody rigidbody;

    Vector3[] enhanceAfterImageDirection = new Vector3[4]
        { new Vector3(1, 0, 1), new Vector3(1, 0, -1),      // 각각 바라보는 방향이 45도일 때, 135도일 때,
          new Vector3(-1, 0, -1), new Vector3(-1, 0, 1) };  // 225도일 때, 315도일 때 적용

    [SerializeField] rotationYAngle rotationYAngle = rotationYAngle.is45; //인스펙터창에서 직접 "반드시" 조정이 필요

    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        switch (rotationYAngle) {
            case (rotationYAngle.is45):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[0].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[0] * speed;
                Debug.Log("is45 됨");
                break;
            case (rotationYAngle.is135):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[1].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[1] * speed;
                Debug.Log("is135 됨");
                break;
            case (rotationYAngle.is225):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[2].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[2] * speed;
                Debug.Log("is225 됨");
                break;
            case (rotationYAngle.is315):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[3].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[3] * speed;
                Debug.Log("is315 됨");
                break;
        }
    }
}