using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [Header("�÷��̾� �̵� �ӵ�")]
    public static float moveSpeed = 16.0f;


    public static float x = 0f, z = 0f;

    Vector3 playerMoveValue;

    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (IsMoving())
            PlayerMoving();
    }



    public static bool IsMoving() {
        if (x != 0 || z != 0) { return true; }
        else { return false; }
    }

    void PlayerMoving() {
        if (!PlayerDashInfermation.isdashing) //��� ���� ��, ����Ű�� �̵� �Ұ�
            transform.position += playerMoveValue
                = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);
        
    }
}