using UnityEngine;
using UnityEngine.UI;

public class DashCooldownCircle : MonoBehaviour
{
    public Image dashCooldownCircle;  // ���� UI �̹��� ����
    public PlayerDash playerDashInfo;  // PlayerDashInfermation ��ũ��Ʈ ����

    void Update()
    {
        // ��� ��Ÿ�ӿ� ���� ���� �׷��� ä���
        if (playerDashInfo != null && dashCooldownCircle != null)
        {
            // ��� ������ �ð��� ��ü ��� ��Ÿ������ ���� ��
            float fillAmount = PlayerDashInfermation.redashCurrentTime / PlayerDashInfermation.redashConstraintTime;



            // fillAmount ���� 0���� 1�� ���� ���ؾ� �մϴ�.
            dashCooldownCircle.fillAmount = fillAmount;
        }
    }
}