using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [Header("�÷��̾� �̵� �ӵ�")]
    [SerializeField] protected static float moveSpeed = 2.0f;


    public static float x = 0f, z = 0f;

    Vector3 playerMoveValue;

    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (IsMoving(ref x, ref z))
            PlayerMoving(ref x, ref z);
    }



    bool IsMoving(ref float x, ref float z) {
        if (x != 0 || z != 0) { return true; }
        else { return false; }
    }

    void PlayerMoving(ref float x, ref float z) {
        if (!PlayerDash.isdashing) //��� ���� ��, ����Ű�� �̵� �Ұ�
            transform.position += playerMoveValue 
                = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);
    }
}