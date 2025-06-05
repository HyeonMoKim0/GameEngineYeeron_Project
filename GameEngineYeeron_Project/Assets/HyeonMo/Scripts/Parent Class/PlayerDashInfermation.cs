using UnityEngine;

public class PlayerDashInfermation : MonoBehaviour {
    [Header("플레이어 대시")]
    //추후 아래 사항들은 protected으로 변경하고, 싱글톤 패턴으로 Instance 제작할 것
    [SerializeField] public int dashCurrentStack = 2;
    public int dashMinStack = 0;
    [SerializeField] public static int dashMaxStack = 2;


    [SerializeField] public float dashSpeed = 32.0f;


    public float dashCurrentDelay = 1.0f;
    [SerializeField] public float dashAfterDelay = 1.0f;


    public float redashCurrentTime = 0f;
    [SerializeField] public static float redashConstraintTime = 5.0f;
}
