using UnityEngine;

public class PlayerMove : PlayerInfermation {
    public static float x = 0f, z = 0f;
    
    Vector3 playerMoveValue;


    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        //isInputDash = Input.GetButton("Fire3"); //GetButton�� ���� �ν��� �ߵǾ� ����


        if (IsMoving(ref x, ref z))
            PlayerMoving(ref x, ref z);

    }



    bool IsMoving(ref float x, ref float z) {
        if (x != 0 || z != 0) {
            Debug.Log("Ismoving true����");
            return true;
        }
        else {
            Debug.Log("Ismoving false����");
            return false;
        }
    }

    void PlayerMoving(ref float x, ref float z) {
        if (!PlayerDash.isdashing) //��� ���� ��, ����Ű�� �̵� �Ұ�
            player.transform.position += playerMoveValue 
                = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);

        Debug.Log("PlayerMoving �ߵǱ���");
    }
}

public class PlayerInfermation : MonoBehaviour {
    [Header("�÷��̾� �⺻ ����")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected int hp;
    [SerializeField] protected int attackPower;
    [SerializeField] protected float moveSpeed = 2.0f;
}