using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스

public class PlayerDashDisplay: MonoBehaviour
{
    public PlayerDashInfermation playerDashInfo;  // PlayerDashInfermation 스크립트 연결
    public TextMeshProUGUI dashCountText;         // TMP UI 텍스트 연결

    void Update()
    {
        // 대시 횟수 텍스트 업데이트
        if (playerDashInfo != null && dashCountText != null)
        {
            dashCountText.text = $"Dash: {PlayerDashInfermation.dashCurrentStack}";
        }
    }
}
