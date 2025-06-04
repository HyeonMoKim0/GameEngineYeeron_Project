using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    bool isInputDash;
    public static bool isdashing = false;

    Vector3 playerDashValue;

    public Rigidbody rigidbody;
    public Collider collider;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        collider = this.gameObject.GetComponent<Collider>();
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
            StartCoroutine(StopDash());
            StartCoroutine("ShotInvicible");
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;
            
        }
    }

    IEnumerator StopDash(){
        //if (collider.enabled == true && PlayerInfermation.isHit)   //���� ���°� �ƴ� ��� ����
        //    ShotInvicible();

        yield return new WaitForSeconds(0.2f);  //��ô� 0.2�� ���� ������ �̵��ϰ� ��

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }

    IEnumerable ShotInvicible() {
        if (collider.enabled == true && PlayerInfermation.isHit) {     //���� ���°� �ƴ� ��� ����
            collider.enabled = false;   //����
            PlayerInfermation.isHit = false;
            yield return new WaitForSeconds(0.2f);

            collider.enabled = true;    //���� ����
            PlayerInfermation.isHit = true;
        }
            
    }
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]

    [SerializeField] public int dashCurrentStack = 2;
                     public int dashMinStack = 0;
    [SerializeField] public static int dashMaxStack = 2;


    [SerializeField] public float dashSpeed = 32.0f;
    

                     public float dashCurrentDelay = 1.0f;
    [SerializeField] public float dashAfterDelay = 1.0f;


                     public float redashCurrentTime = 0f;
    [SerializeField] public static float redashConstraintTime = 5.0f;
}