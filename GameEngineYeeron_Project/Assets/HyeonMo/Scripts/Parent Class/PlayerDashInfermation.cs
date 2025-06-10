using System.Collections;
using UnityEngine;

public enum DashLevel {
    Default = 1,
    Samurai,
    Hunter
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("플레이어 대시")]
    //추후 아래 사항들은 protected으로 변경하고, 싱글톤 패턴으로 Instance 제작할 것
    public static int dashCurrentStack = 2;
    [HideInInspector] public int dashMinStack = 0;
    public static int dashMaxStack = 2;


    public float dashSpeed = 32.0f; //기본 32.0f


    public static float dashCurrentDelay = 1.0f;
    [HideInInspector] public float dashAfterDelay = 1.0f;


    public static float redashCurrentTime = 0f;
    [HideInInspector] public static float redashConstraintTime = 5.0f;

    protected bool isInputDash = false;

    protected Rigidbody rigidbody; //확인용, 추후 private 변경 예정
    protected Collider collider; //확인용, 추후 private 변경 예정

    /*--------------------------------------------------------*/
    public DashLevel currentDashLevel;
    
    public static bool isdashing = false;

    protected Vector3 playerDashValue;

    PlayerDash playerDash;
    SamuraiDash samuraiDash;
    HunterDash hunterDash;

    /*--------------------------------------------------------*/
    private void Start() {
        playerDash = GetComponentInChildren<PlayerDash>();
        samuraiDash = GetComponentInChildren<SamuraiDash>();
        hunterDash = GetComponentInChildren<HunterDash>();
        
        if (playerDash != null) { Debug.Log("찾았다!" + playerDash.gameObject.name); }
        if (samuraiDash != null) { Debug.Log("찾았다!" + samuraiDash.gameObject.name); }
        if (hunterDash != null) { Debug.Log("찾았다!" + hunterDash.gameObject.name); }

        currentDashLevel = DashLevel.Default;
        //currentDashLevel = DashLevel.Samurai;

        SetDash();

        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();

    }

    private void Update() {
        if (dashCurrentStack < dashMaxStack)
        {     //충전된 대시가 2 이상이면 대시 쿨타임 카운팅 시작
            redashCurrentTime += Time.deltaTime;
            dashCurrentDelay += Time.deltaTime;

            if (redashCurrentTime >= redashConstraintTime && dashCurrentStack < dashMaxStack)
            {
                ++dashCurrentStack;
                redashCurrentTime = 0f;
            }
        }
    }

    private void FixedUpdate() {
        isInputDash = Input.GetButton("Fire3"); //GetButton이 제일 인식이 잘되어 선택

        CheckDash();
    }

    void CheckDash() {
        if (isInputDash && PlayerMove.IsMoving()) {
            if (IsNotDelay() && IsExistDashStack()) {
                StartDash();
            }
        }
    }

    void StartDash() {
        --dashCurrentStack;
        dashCurrentDelay = 0f;

        if (collider.enabled == true && PlayerInfermation.isHit)   //무적 상태가 아닐 경우 실행
            StartCoroutine(ShotInvicible());

        isdashing = true;

        switch (currentDashLevel)
        {
            case DashLevel.Samurai:
                samuraiDash.Dash();
                Debug.Log("samuraiDash 실행됨");
                break;

            case DashLevel.Hunter:
                //hunterDash.Dash();
                break;

            default:
                playerDash.Dash();
                Debug.Log("playerDash 실행됨");
                break;
        }
    }


    void SetDash() {
        playerDash.enabled = false;
        samuraiDash.enabled = false;
        hunterDash.enabled = false;

        switch (currentDashLevel) {
            case DashLevel.Samurai:
                dashSpeed /= 2;
                samuraiDash.enabled = true;
                break;
            case DashLevel.Hunter:
                hunterDash.enabled = true;
                break;
            default:
                playerDash.enabled = true;
                break;
        }
    }
    

    protected IEnumerator ShotInvicible()
    {
        collider.enabled = false;   //무적
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //무적 해제
        PlayerInfermation.isHit = true;
        
    }


    protected bool IsNotDelay()
    {
        if (dashCurrentDelay >= dashAfterDelay) { return true; }
        else { return false; }
    }

    protected bool IsExistDashStack()
    {
        if (dashCurrentStack > dashMinStack) { return true; }
        else { return false; }
    }
}
