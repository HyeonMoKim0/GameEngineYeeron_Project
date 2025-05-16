using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float x = 0f, z = 0f;
    float dashDelay = 0f;
    bool isInputDash;
    Vector3 playerMoveValue;
    Vector3 playerDashValue;
    Vector3 playerVelocity;

    [SerializeField]
    GameObject player;
    float moveSpeed = 8.0f;
    float dashSpeed = 8.0f;

    void Start() {
        
    }

    void Update() {
        dashDelay += Time.deltaTime;
        Debug.Log(dashDelay);

        if (dashDelay >= 1 && isInputDash)
        {
            dashDelay = 0f;
            Dash(ref x, ref z);
        }
    }

    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isInputDash = Input.GetButton("Fire3"); //GetButton�� ���� �ν��� �ߵǾ� ����


        if (IsMoving(ref x, ref z))
            PlayerMoving(ref x, ref z);
    }

    void Dash(ref float x, ref float z) {
        player.transform.position += playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);

        Debug.Log("Dash �ߵǱ���");
    }

    bool IsMoving(ref float x, ref float z) {
        if (x != 0 || z != 0)
        {
            Debug.Log("Ismoving true����");
            return true;
        }

        else {
            Debug.Log("Ismoving false����");
            return false;
        }
    }

    void PlayerMoving(ref float x, ref float z) {
        player.transform.position += playerMoveValue = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);
        Debug.Log("PlayerMoving �ߵǱ���");
    }
}
