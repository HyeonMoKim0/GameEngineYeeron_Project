using UnityEngine;

public class PlayerMove : PlayerInfermation {
    public static float x = 0f, z = 0f;
    
    Vector3 playerMoveValue;


    void FixedUpdate() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        //isInputDash = Input.GetButton("Fire3"); //GetButton이 제일 인식이 잘되어 선택


        if (IsMoving(ref x, ref z))
            PlayerMoving(ref x, ref z);

    }



    bool IsMoving(ref float x, ref float z) {
        if (x != 0 || z != 0) {
            Debug.Log("Ismoving true구여");
            return true;
        }
        else {
            Debug.Log("Ismoving false구여");
            return false;
        }
    }

    void PlayerMoving(ref float x, ref float z) {
        if (!PlayerDash.isdashing) //대시 중일 때, 조작키로 이동 불가
            player.transform.position += playerMoveValue 
                = new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, z * moveSpeed * Time.fixedDeltaTime);

        Debug.Log("PlayerMoving 잘되구여");
    }
}

public class PlayerInfermation : MonoBehaviour {
    [Header("플레이어 기본 정보")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected int hp;
    [SerializeField] protected int attackPower;
    [SerializeField] protected float moveSpeed = 2.0f;
}