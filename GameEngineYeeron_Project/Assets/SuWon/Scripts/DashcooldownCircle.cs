using UnityEngine;
using UnityEngine.UI;

public class DashCooldownCircle : MonoBehaviour
{
    public Image dashCooldownCircle;  // 원형 UI 이미지 연결
    public PlayerDash playerDashInfo;  // PlayerDashInfermation 스크립트 연결

    void Update()
    {
        // 대시 쿨타임에 맞춰 원형 그래프 채우기
        if (playerDashInfo != null && dashCooldownCircle != null)
        {
            // 대시 재충전 시간을 전체 대시 쿨타임으로 나눈 값
            float fillAmount = PlayerDashInfermation.redashCurrentTime / PlayerDashInfermation.redashConstraintTime;



            // fillAmount 값이 0에서 1로 점차 변해야 합니다.
            dashCooldownCircle.fillAmount = fillAmount;
        }
    }
}