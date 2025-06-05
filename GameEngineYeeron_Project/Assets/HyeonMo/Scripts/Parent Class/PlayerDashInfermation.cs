using UnityEngine;

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
