//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EnhanceAfterImageManager : MonoBehaviour {
    int index = 0; //반복문 사용용

    [SerializeField] GameObject enhanceAfterImages; //enhanceAfterImage 프리팹 할당

    Rigidbody rigidbody;

    Vector3[] enhanceAfterImageDirection = new Vector3[4]
        { new Vector3(1, 0, 1), new Vector3(-1, 0, 1),
          new Vector3(1, 0, -1), new Vector3(-1, 0, -1) };


    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        for (index = 0; index < 4; ++index)
        {
            enhanceAfterImageDirection[index] = enhanceAfterImageDirection[index].normalized; //각각 방향 정규화
        }

        Destroy(gameObject, 5f); //5초 뒤 해당 enhanceAfterImage가 사라짐
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }
}
