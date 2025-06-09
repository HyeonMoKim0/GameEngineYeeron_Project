using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum rotationYAngle {
    is45 = 1,
    is135,
    is225,
    is315
}

public class EnhanceAfterImageMove : MonoBehaviour {
    int index = 0; //�ݺ��� ����

    float rotationY = 0;
    //float[] rotationYAngle = new float[4] { 45f, 135f, 225f, 315f };
    float speed = 4f;


    Rigidbody rigidbody;

    Vector3[] enhanceAfterImageDirection = new Vector3[4]
        { new Vector3(1, 0, 1), new Vector3(1, 0, -1),
          new Vector3(-1, 0, -1), new Vector3(-1, 0, 1) };

    [SerializeField] rotationYAngle rotationYAngle = rotationYAngle.is45; //�ν�����â���� ���� "�ݵ��" ������ �ʿ�

    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        //for (index = 0; index < 4; ++index) {
        //    enhanceAfterImageDirection[index] = enhanceAfterImageDirection[index].normalized; //���� ���� ����ȭ
        //}

        rotationY = (float)this.transform.rotation.y;
        Debug.Log(rotationY);

        switch (rotationYAngle) {
            case (rotationYAngle.is45):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[0].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[0] * speed;
                Debug.Log("is45 ��");
                break;
            case (rotationYAngle.is135):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[1].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[1] * speed;
                Debug.Log("is135 ��");
                break;
            case (rotationYAngle.is225):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[2].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[2] * speed;
                Debug.Log("is225 ��");
                break;
            case (rotationYAngle.is315):
                enhanceAfterImageDirection[0] = enhanceAfterImageDirection[3].normalized;
                rigidbody.velocity = enhanceAfterImageDirection[3] * speed;
                Debug.Log("is315 ��");
                break;
        }

        //Destroy(transform.parent.gameObject, 5f);
    }
}
