using UnityEngine;
using TMPro;  // TextMeshPro ���ӽ����̽�

public class PlayerHPDisplay : MonoBehaviour
{
    public PlayerInfermation playerInfo;  // �÷��̾� ���� ��ũ��Ʈ ����
    public TextMeshProUGUI healthText;    // TMP UI �ؽ�Ʈ ����

    void Update()
    {
        if (playerInfo != null && healthText != null)
        {
            healthText.text = $"HP: {playerInfo.health}";
        }
    }
}
