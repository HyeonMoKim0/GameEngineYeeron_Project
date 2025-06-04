using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerDash : PlayerDashInfermation {
    bool isInputDash;
    public static bool isdashing = false;

    Vector3 playerDashValue;

    public Rigidbody rigidbody; //확인용, 추후 private 변경 예정
    public Collider collider; //확인용, 추후 private 변경 예정

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
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

        if (isInputDash && PlayerMove.IsMoving()) {
            Dash(ref PlayerMove.x, ref PlayerMove.z);
        }
    }

    void Dash(ref float x, ref float z) {
        //아래는 텔포하는 대시 기능
        //player.transform.position += playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
        
        if (IsNotDelay() && IsExistDashStack()) {
            --dashCurrentStack;
            dashCurrentDelay = 0f;
            Debug.Log(redashCurrentTime);

            StartCoroutine(StopDash());
            if (collider.enabled == true && PlayerInfermation.isHit)   //무적 상태가 아닐 경우 실행
                StartCoroutine(ShotInvicible());
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            rigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;
            
        }
    }

    IEnumerator StopDash(){
        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        rigidbody.velocity = Vector3.zero;
        isdashing = false;
    }

    IEnumerator ShotInvicible() {
        collider.enabled = false;   //무적
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //무적 해제
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
    [Header("플레이어 대시")]

    [SerializeField] protected int dashCurrentStack = 2;
                     protected int dashMinStack = 0;
    [SerializeField] protected static int dashMaxStack = 2;


    [SerializeField] protected float dashSpeed = 32.0f;
    

                     protected float dashCurrentDelay = 1.0f;
    [SerializeField] protected float dashAfterDelay = 1.0f;


                     protected float redashCurrentTime = 0f;
    [SerializeField] protected static float redashConstraintTime = 5.0f;
}