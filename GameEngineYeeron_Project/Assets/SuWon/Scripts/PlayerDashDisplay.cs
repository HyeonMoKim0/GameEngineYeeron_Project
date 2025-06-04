using UnityEngine;
using TMPro; // TextMeshPro 사용을 위해 추가

public class PlayerDashDisplay : MonoBehaviour
{
    public PlayerDash playerDash;  // PlayerDash 스크립트 연결
    public TextMeshProUGUI dashCountText;  // 대시 횟수 표시 UI 텍스트 연결

    void Update()
    {
        // 대시 횟수를 UI에 실시간으로 반영
        if (playerDash != null && dashCountText != null)
        {
            dashCountText.text = "Dash: " + playerDash.dashCurrentStack.ToString();
        }
    }
}