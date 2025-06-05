using UnityEngine;

public class PlayerDashInfermation : MonoBehaviour {
    [Header("�÷��̾� ���")]
    //���� �Ʒ� ���׵��� protected���� �����ϰ�, �̱��� �������� Instance ������ ��
    [HideInInspector] public int dashCurrentStack = 2;
    [HideInInspector] public int dashMinStack = 0;
    [HideInInspector] public static int dashMaxStack = 2;


    public float dashSpeed = 32.0f;


    [HideInInspector] public float dashCurrentDelay = 1.0f;
    [HideInInspector] public float dashAfterDelay = 1.0f;


    [HideInInspector] public float redashCurrentTime = 0f;
    [HideInInspector] public static float redashConstraintTime = 5.0f;
}
