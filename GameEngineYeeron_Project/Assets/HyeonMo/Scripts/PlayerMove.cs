using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float x, z;
    bool isInputDash;
    bool isMoving;
    Vector3 playerMoveValue;
    Vector3 playerDashValue;
    Vector3 playerVelocity;

    [SerializeField]
    GameObject player;
    Rigidbody playerRigidbody;
    float moveSpeed = 8.0f;
    float dashSpeed = 8.0f;

    void Start() {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isInputDash = Input.GetButtonDown("Fire3");

        if (IsMoving(ref x, ref z))
            PlayerMoving(ref x, ref z);

        if (isInputDash)
            Dash(ref x, ref z);
            
    }

    void Dash(ref float x, ref float z) {
        player.transform.position += playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
        //이새끼 왜 대각선으로만가는데?

        Debug.Log("Dash 잘되구여");
    }

    bool IsMoving(ref float x, ref float z) {
        if (x != 0 || z != 0)
        {
            Debug.Log("Ismoving true구여");
            return true;
        }

        else {
            Debug.Log("Ismoving false구여");
            return false;
        }
    }

    void PlayerMoving(ref float x, ref float z) {
        player.transform.position += playerMoveValue = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);
        Debug.Log("PlayerMoving 잘되구여");
    }
}
