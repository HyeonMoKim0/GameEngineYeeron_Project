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
            playerDashValue = new Vector3(x * dashSpeed, 0, z * dashSpeed);
            playerRigidbody.AddForce(playerDashValue, ForceMode.Impulse);
            isdashing = true;
            StartCoroutine(StopDash());
            Debug.Log("Dash 잘되구여");
        }
    }

    IEnumerator StopDash(){
        yield return new WaitForSeconds(0.2f);  //대시는 0.2초 동안 빠르게 이동하게 됨

        playerRigidbody.velocity = Vector3.zero;
        isdashing = false;
    }
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("플레이어 대시")]
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