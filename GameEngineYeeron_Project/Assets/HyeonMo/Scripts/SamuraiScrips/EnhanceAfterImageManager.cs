//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EnhanceAfterImageManager : MonoBehaviour {
    int index = 0; //�ݺ��� ����

    [SerializeField] GameObject enhanceAfterImages; //enhanceAfterImage ������ �Ҵ�

    Rigidbody rigidbody;

    Vector3[] enhanceAfterImageDirection = new Vector3[4]
        { new Vector3(1, 0, 1), new Vector3(-1, 0, 1),
          new Vector3(1, 0, -1), new Vector3(-1, 0, -1) };


    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        for (index = 0; index < 4; ++index)
        {
            enhanceAfterImageDirection[index] = enhanceAfterImageDirection[index].normalized; //���� ���� ����ȭ
        }

        Destroy(gameObject, 5f); //5�� �� �ش� enhanceAfterImage�� �����
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }
}
