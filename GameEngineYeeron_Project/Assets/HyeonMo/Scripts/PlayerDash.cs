using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    bool isInputDash;
    public static bool isdashing = false;

    Vector3 playerDashValue;

    Rigidbody playerRigidbody;

    void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (dashCurrentStack < dashMaxStack) {     //������ ��ð� 2 �̻��̸� ��� ��Ÿ�� ī���� ����
            redashCurrentTime += Time.deltaTime;
            dashCurrentDelay += Time.deltaTime;

            if (redashCurrentTime >= redashConstraintTime && dashCurrentStack < dashMaxStack) {
                ++dashCurrentStack;
                redashCurrentTime = 0f;
            }
        }
    }

    void FixedUpdate() {
        isInputDash = Input.GetButton("Fire3"); //GetButton�� ���� �ν��� �ߵǾ� ����

        if (isInputDash && dashCurrentDelay >= dashAfterDelay && dashCurrentStack > dashMinStack) {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);
            Dash(ref PlayerMove.x, ref PlayerMove.z);
        }
    }

    void Dash(ref float x, ref float z) {
        //�Ʒ��� �����ϴ� ��� ���
        //player.transform.position += playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);

        if (x != 0 || z != 0) {
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            playerRigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;
            StartCoroutine(StopDash());
            Debug.Log("Dash �ߵǱ���");
        }
    }

    IEnumerator StopDash(){
        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        playerRigidbody.velocity = Vector3.zero;
        isdashing = false;
    }
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]
    [SerializeField] protected GameObject player;
    
    [SerializeField] protected int dashCurrentStack = 2;
                     protected int dashMinStack = 0;
    [SerializeField] protected int dashMaxStack = 2;


    [SerializeField] protected float dashSpeed = 32.0f;
    
                     protected float dashCurrentDelay = 1.0f;
    [SerializeField] protected float dashAfterDelay = 1.0f;

                     protected float redashCurrentTime = 0f;
    [SerializeField] protected float redashConstraintTime = 5.0f;
}