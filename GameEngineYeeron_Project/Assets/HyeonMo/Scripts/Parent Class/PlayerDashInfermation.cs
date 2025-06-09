using System.Collections;
using UnityEngine;

public enum DashLevel {
    Default = 1,
    Samurai,
    Hunter
}

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]
    //���� �Ʒ� ���׵��� protected���� �����ϰ�, �̱��� �������� Instance ������ ��
    public static int dashCurrentStack = 2;
    [HideInInspector] public int dashMinStack = 0;
    public static int dashMaxStack = 2;


    public float dashSpeed = 32.0f; //�⺻ 32.0f


    public static float dashCurrentDelay = 1.0f;
    [HideInInspector] public float dashAfterDelay = 1.0f;


    public static float redashCurrentTime = 0f;
    [HideInInspector] public static float redashConstraintTime = 5.0f;

    protected bool isInputDash = false;

    protected Rigidbody rigidbody; //Ȯ�ο�, ���� private ���� ����
    protected Collider collider; //Ȯ�ο�, ���� private ���� ����

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
        
        if (playerDash != null) { Debug.Log("ã�Ҵ�!" + playerDash.gameObject.name); }
        if (samuraiDash != null) { Debug.Log("ã�Ҵ�!" + samuraiDash.gameObject.name); }
        if (hunterDash != null) { Debug.Log("ã�Ҵ�!" + hunterDash.gameObject.name); }

        currentDashLevel = DashLevel.Default;
        //currentDashLevel = DashLevel.Samurai;

        SetDash();

        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();

    }

    private void Update() {
        if (dashCurrentStack < dashMaxStack)
        {     //������ ��ð� 2 �̻��̸� ��� ��Ÿ�� ī���� ����
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
        isInputDash = Input.GetButton("Fire3"); //GetButton�� ���� �ν��� �ߵǾ� ����

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

        if (collider.enabled == true && PlayerInfermation.isHit)   //���� ���°� �ƴ� ��� ����
            StartCoroutine(ShotInvicible());

        isdashing = true;

        switch (currentDashLevel)
        {
            case DashLevel.Samurai:
                samuraiDash.Dash();
                Debug.Log("samuraiDash �����");
                break;

            case DashLevel.Hunter:
                //hunterDash.Dash();
                break;

            default:
                playerDash.Dash();
                Debug.Log("playerDash �����");
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
        collider.enabled = false;   //����
        PlayerInfermation.isHit = false;
        yield return new WaitForSeconds(0.2f);

        collider.enabled = true;    //���� ����
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
