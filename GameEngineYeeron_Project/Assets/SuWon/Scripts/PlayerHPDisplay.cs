using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽�

public class PlayerHPDisplay_TMP : MonoBehaviour
{
    public PlayerInfermation playerInfo;  // PlayerInfermation ��ũ��Ʈ ����
    public TextMeshProUGUI healthText;    // TMP UI �ؽ�Ʈ ����

    void Update()
    {
        // �÷��̾� HP �ؽ�Ʈ ������Ʈ
        if (playerInfo != null && healthText != null)
        {
            healthText.text = $"HP: {PlayerInfermation.health}";
        }
    }
}