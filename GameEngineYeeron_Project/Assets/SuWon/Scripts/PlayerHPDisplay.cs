using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스

public class PlayerHPDisplay : MonoBehaviour
{
    public PlayerInfermation playerInfo;  // 플레이어 정보 스크립트 연결
    public TextMeshProUGUI healthText;    // TMP UI 텍스트 연결

    void Update()
    {
        if (playerInfo != null && healthText != null)
        {
            healthText.text = $"HP: {playerInfo.health}";
        }
    }
}
