using UnityEngine;

public class PlayerDashInfermation : MonoBehaviour {
    [Header("플레이어 대시")]
    //추후 아래 사항들은 protected으로 변경하고, 싱글톤 패턴으로 Instance 제작할 것
    [HideInInspector] public int dashCurrentStack = 2;
    [HideInInspector] public int dashMinStack = 0;
    [HideInInspector] public static int dashMaxStack = 2;


    public float dashSpeed = 32.0f;


    [HideInInspector] public float dashCurrentDelay = 1.0f;
    [HideInInspector] public float dashAfterDelay = 1.0f;


    [HideInInspector] public float redashCurrentTime = 0f;
    [HideInInspector] public static float redashConstraintTime = 5.0f;
}
