using UnityEngine;

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]
    //���� �Ʒ� ���׵��� protected���� �����ϰ�, �̱��� �������� Instance ������ ��
    [SerializeField] public int dashCurrentStack = 2;
    public int dashMinStack = 0;
    [SerializeField] public static int dashMaxStack = 2;


    [SerializeField] public float dashSpeed = 32.0f;


    public float dashCurrentDelay = 1.0f;
    [SerializeField] public float dashAfterDelay = 1.0f;


    public float redashCurrentTime = 0f;
    [SerializeField] public static float redashConstraintTime = 5.0f;
}
