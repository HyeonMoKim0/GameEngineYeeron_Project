using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    bool isInputDash;
    public static bool isdashing = false;

    Vector3 playerDashValue;

    public Rigidbody rigidbody; //Ȯ�ο�, ���� private ���� ����
    public Collider collider; //Ȯ�ο�, ���� private ���� ����

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
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

        if (isInputDash && PlayerMove.IsMoving()) {
            Dash(ref PlayerMove.x, ref PlayerMove.z);
        }
    }

    void Dash(ref float x, ref float z) {
        //�Ʒ��� �����ϴ� ��� ���
        //player.transform.position += playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
        
        if (IsNotDelay() && IsExistDashStack()) {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);

            StartCoroutine(StopDash());
            if (collider.enabled == true && PlayerInfermation.isHit)   //���� ���°� �ƴ� ��� ����
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;
            
        }
    }

    IEnumerator StopDash(){
        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }

    IEnumerator ShotInvicible() {
        collider.enabled = false;   //����
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //���� ����
        PlayerInfermation.isHit = true;
    }

    bool IsNotDelay() {
        if (dashCurrentDelay >= dashAfterDelay) { return true; }
        else { return false; }
    }

    bool IsExistDashStack() {
        if (dashCurrentStack > dashMinStack) { return true; }
        else { return false; }
    }
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]

    [SerializeField] protected int dashCurrentStack = 2;
                     protected int dashMinStack = 0;
    [SerializeField] protected static int dashMaxStack = 2;


    [SerializeField] protected float dashSpeed = 32.0f;
    

                     protected float dashCurrentDelay = 1.0f;
    [SerializeField] protected float dashAfterDelay = 1.0f;


                     protected float redashCurrentTime = 0f;
    [SerializeField] protected static float redashConstraintTime = 5.0f;
}