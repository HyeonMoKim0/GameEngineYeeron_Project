using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스

public class PlayerHPDisplay_TMP : MonoBehaviour
{
    public PlayerInfermation playerInfo;  // PlayerInfermation 스크립트 연결
    public TextMeshProUGUI healthText;    // TMP UI 텍스트 연결

    void Update()
    {
        // 플레이어 HP 텍스트 업데이트
        if (playerInfo != null && healthText != null)
        {
            healthText.text = $"HP: {PlayerInfermation.health}";
        }
    }
}