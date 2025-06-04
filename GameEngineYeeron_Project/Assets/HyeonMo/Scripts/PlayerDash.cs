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
        if (dashCurrentStack < dashMaxStack) {     //충전된 대시가 2 이상이면 대시 쿨타임 카운팅 시작
            redashCurrentTime += Time.deltaTime;
            dashCurrentDelay += Time.deltaTime;

            if (redashCurrentTime >= redashConstraintTime && dashCurrentStack < dashMaxStack) {
                ++dashCurrentStack;
                redashCurrentTime = 0f;
            }
        }
    }

    void FixedUpdate() {
        isInputDash = Input.GetButton("Fire3"); //GetButton이 제일 인식이 잘되어 선택

        if (isInputDash && dashCurrentDelay >= dashAfterDelay && dashCurrentStack > dashMinStack) {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);
            Dash(ref PlayerMove.x, ref PlayerMove.z);
        }
    }

    void Dash(ref float x, ref float z) {
        //아래는 텔포하는 대시 기능
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
        //if (collider.enabled == true && PlayerInfermation.isHit)   //무적 상태가 아닐 경우 실행
        //    ShotInvicible();

        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }

    IEnumerable ShotInvicible() {
        if (collider.enabled == true && PlayerInfermation.isHit) {     //무적 상태가 아닐 경우 실행
            collider.enabled = false;   //무적
            PlayerInfermation.isHit = false;
            yield return new WaitForSeconds(0.2f);

            collider.enabled = true;    //무적 해제
            PlayerInfermation.isHit = true;
        }
            
    }
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("플레이어 대시")]

    [SerializeField] public int dashCurrentStack = 2;
                     public int dashMinStack = 0;
    [SerializeField] public static int dashMaxStack = 2;


    [SerializeField] public float dashSpeed = 32.0f;
    

                     public float dashCurrentDelay = 1.0f;
    [SerializeField] public float dashAfterDelay = 1.0f;


                     public float redashCurrentTime = 0f;
    [SerializeField] public static float redashConstraintTime = 5.0f;
}